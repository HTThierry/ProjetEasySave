using EasySave.lib.Models;
using System.Diagnostics;

namespace EasySave.lib.Services
{
    public class SaveWork
    {
        public SaveWorkModel _SaveWorkModel = new SaveWorkModel();

        public string[] GetInstanceInfo()
        {
            string[] AttributsForPresentation = new string[4] { _SaveWorkModel.NameSaveWork, $"{_SaveWorkModel.TypeSaveWork}", _SaveWorkModel.SourcePathSaveWork, _SaveWorkModel.DestinationPathSaveWork };
            return AttributsForPresentation;
        }

        public int LaunchSaveWork()
        {
            if (_SaveWorkModel.TypeSaveWork == 1)
            {
                return CompleteCopyFiles();
            }
            else
            {
                return DifferentialCopyFiles();
            }
        }

        private int CompleteCopyFiles()
        {
            string SourcePath = _SaveWorkModel.SourcePathSaveWork;
            string DestinationPath = _SaveWorkModel.DestinationPathSaveWork;
            int TotalFilesToCopy = 0;
            long TotalFilesSizeToCopy = 0;
            int NbFilesLeft = 0;
            long FilesSizeLeft = 0;
            int LogReturnCode = 0;
            int ProgressStateReturnCode = 0;

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (!Directory.Exists(DestinationPath))
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
                    }

                    string[] files = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
                    TotalFilesToCopy = files.Length;
                    NbFilesLeft = TotalFilesToCopy;

                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        TotalFilesSizeToCopy += fileInfo.Length;
                    }
                    FilesSizeLeft = TotalFilesSizeToCopy;

                    foreach (string file in files)
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        try
                        {
                            File.Copy(file, file.Replace(SourcePath, DestinationPath), true);
                        }
                        catch
                        {
                            return 1;
                        }
                        stopwatch.Stop();
                        double FileTransferTime = stopwatch.Elapsed.TotalSeconds;

                        NbFilesLeft--;
                        FileInfo fileInfo = new FileInfo(file);
                        FilesSizeLeft -= fileInfo.Length;

                        LogReturnCode += Log.LogFiles(LogArrayCreator(file, DestinationPath, FileTransferTime));
                        ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Active", TotalFilesToCopy, TotalFilesSizeToCopy, NbFilesLeft, FilesSizeLeft, file, DestinationPath));
                    }
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Inactive", 0, 0, 0, 0, "", ""));
                }
                else
                {
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Inactive", 0, 0, 0, 0, "", ""));
                }
                if (LogReturnCode >= 1 || ProgressStateReturnCode >= 1)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                 //A retirer après test
                return 1;
            }
        }

        private string[] LogArrayCreator(string FilePath, string DestinationPath, double FileTransferTime)
        {
            DateTime today = DateTime.Now;
            FileInfo _FileInfo = new FileInfo(FilePath);
            long FileSize = _FileInfo.Length;
            string FileDestinationPath = Path.Combine(DestinationPath, Path.GetFileName(FilePath));

            string[] LogArray = new string[]
            {
                Path.GetFileName(FilePath),
                FilePath,
                FileDestinationPath,
                DestinationPath,
                $"{FileSize}",
                $"{FileTransferTime}",
                today.ToString("dd/MM/yyyy hh:mm:ss")
            };

            return LogArray;
        }

        private string[] ProgressArrayCreator(string ProgressState, int TotalFilesToCopy, long TotalFilesSizeToCopy, int NbFilesLeft, long FilesSizeLeft, string FilePath, string DestinationPath)
        {
            DateTime today = DateTime.Now;
            string FileDestinationPath = "";
            if (ProgressState == "Active")
            {
                FileDestinationPath = Path.Combine(DestinationPath, Path.GetFileName(FilePath));
            }

            string[] ProgressArray = new string[] {
                        _SaveWorkModel.NameSaveWork,
                        today.ToString("dd/MM/yyyy hh:mm:ss"),
                        ProgressState,
                        $"{TotalFilesToCopy}",
                        $"{TotalFilesSizeToCopy}",
                        $"{NbFilesLeft}",
                        $"{FilesSizeLeft}",
                        FilePath,
                        FileDestinationPath
                        };
            
            return ProgressArray;
        }

        private int DifferentialCopyFiles()
        {
            string SourcePath = _SaveWorkModel.SourcePathSaveWork;
            string DestinationPath = _SaveWorkModel.DestinationPathSaveWork;
            int TotalFilesToCopy = 0;
            long TotalFilesSizeToCopy = 0;
            int NbFilesLeft = 0;
            long FilesSizeLeft = 0;
            int LogReturnCode = 0;
            int ProgressStateReturnCode = 0;

            
            try
            {
                if (Directory.Exists(_SaveWorkModel.SourcePathSaveWork))
                {
                    if (!Directory.Exists(DestinationPath))
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }
                    
                    foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
                    }

                    string[] files = Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories);
                    TotalFilesToCopy = files.Length;
                    NbFilesLeft = TotalFilesToCopy;

                    foreach (string file in files)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        TotalFilesSizeToCopy += fileInfo.Length;
                    }
                    FilesSizeLeft = TotalFilesSizeToCopy;

                    foreach (string file in files)
                    {
                        if (File.GetLastWriteTime(file) > File.GetLastWriteTime(file.Replace(SourcePath, DestinationPath)))
                        {
                            var stopwatch = Stopwatch.StartNew();
                            try
                            {
                                File.Copy(file, file.Replace(SourcePath, DestinationPath), true);

                            }
                            catch
                            {
                                return 1;
                            }
                            stopwatch.Stop();
                            double FileTransferTime = stopwatch.Elapsed.TotalSeconds;

                            NbFilesLeft--;
                            FileInfo fileInfo = new FileInfo(file);
                            FilesSizeLeft -= fileInfo.Length;

                            LogReturnCode += Log.LogFiles(LogArrayCreator(file, DestinationPath, FileTransferTime));
                            ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Active", TotalFilesToCopy, TotalFilesSizeToCopy, NbFilesLeft, FilesSizeLeft, file, DestinationPath));
                        };
                    }
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Inactive", 0, 0, 0, 0, "", ""));
                }
                else
                {
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator("Inactive", 0, 0, 0, 0, "", ""));
                }
                if (LogReturnCode >= 1 || ProgressStateReturnCode >= 1)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                                             //A retirer après test
                return 1;
            }
        }
    }
}
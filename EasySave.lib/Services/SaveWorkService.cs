using EasySave.lib.Models;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace EasySave.lib.Services
{
    public class SaveWorkService
    {
        //public SaveWorkModel _SaveWorkModel { get; set; } = new SaveWorkModel();
        //public cryptoSoft _cryptoSoft = new cryptoSoft();
        //public RunningProcess _RunningProcess = new RunningProcess();

        //public string[] GetInstanceInfo()
        //{
        //    string[] AttributsForPresentation = new string[4] { _SaveWorkModel.NameSaveWork, $"{_SaveWorkModel.TypeSaveWork}", _SaveWorkModel.SourcePathSaveWork, _SaveWorkModel.DestinationPathSaveWork };
        //    return AttributsForPresentation;
        //}

        public int LaunchSaveWork(SaveWorkModel model)
        {
            if (model.TypeSaveWork == 1)
            {
                return CompleteCopyFiles(model);
            }
            else
            {
                return DifferentialCopyFiles(model);
            }
        }

        private int CompleteCopyFiles(SaveWorkModel model)
        {
            string SourcePath = model.SourcePathSaveWork;
            string DestinationPath = model.DestinationPathSaveWork;
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
                        int timeForCryp = 0;


                        string FileDestinationPath = Path.Combine(DestinationPath, Path.GetFileName(file));
                        string extentionOfFile = Path.GetExtension(file);
                        string FilesToCryp = ConfigurationManager.AppSettings["fileToCryp"];
                        string[] extensionsToCryp = FilesToCryp.Split(',');
                        bool isFileToCryp = false;

                        foreach (string extension in extensionsToCryp)
                        {
                            if (extentionOfFile == extension)
                            {
                                isFileToCryp = true;
                                break;
                            }
                        }
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        try
                        {

                            if (isFileToCryp)
                            {
                               timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{FileDestinationPath}");
                                var test = timeForCryp;
                            }
                            else
                            {
                                File.Copy(file, file.Replace(SourcePath, DestinationPath), true);
                            }
                            
                            

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

                        LogReturnCode += Log.LogFiles(LogArrayCreator(file, DestinationPath, FileTransferTime, timeForCryp));
                        ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model, "Active", TotalFilesToCopy, TotalFilesSizeToCopy, NbFilesLeft, FilesSizeLeft, file, DestinationPath));
                    }
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model, "Inactive", 0, 0, 0, 0, "", ""));
                }
                else
                {
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model, "Inactive", 0, 0, 0, 0, "", ""));
                }
                if (LogReturnCode >= 1 || ProgressStateReturnCode >= 1)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        private string[] LogArrayCreator(string FilePath, string DestinationPath, double FileTransferTime,int timeForCryp)
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
                $"{timeForCryp}",
                $"{FileSize}",
                $"{FileTransferTime}",
                today.ToString("dd/MM/yyyy hh:mm:ss")
            };

            return LogArray;
        }

        private string[] ProgressArrayCreator(SaveWorkModel model,string ProgressState, int TotalFilesToCopy, long TotalFilesSizeToCopy, int NbFilesLeft, long FilesSizeLeft, string FilePath, string DestinationPath)
        {
            DateTime today = DateTime.Now;
            string FileDestinationPath = "";
            if (ProgressState == "Active")
            {
                FileDestinationPath = Path.Combine(DestinationPath, Path.GetFileName(FilePath));
            }

            string[] ProgressArray = new string[] {
                        model.NameSaveWork,
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

        private int DifferentialCopyFiles(SaveWorkModel model)
        {
            string SourcePath = model.SourcePathSaveWork;
            string DestinationPath = model.DestinationPathSaveWork;
            int TotalFilesToCopy = 0;
            long TotalFilesSizeToCopy = 0;
            int NbFilesLeft = 0;
            long FilesSizeLeft = 0;
            int LogReturnCode = 0;
            int ProgressStateReturnCode = 0;



            try
            {
                if (Directory.Exists(model.SourcePathSaveWork))
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
                            int timeForCryp = 0;


                            string FileDestinationPath = Path.Combine(DestinationPath, Path.GetFileName(file));
                            string extentionOfFile = Path.GetExtension(file);
                            string FilesToCryp = ConfigurationManager.AppSettings["fileToCryp"];
                            string[] extensionsToCryp = FilesToCryp.Split(',');
                            bool isFileToCryp = false;

                            foreach (string extension in extensionsToCryp)
                            {
                                if (extentionOfFile == extension)
                                {
                                    isFileToCryp = true;
                                    break;
                                }
                            }

                            var stopwatch = Stopwatch.StartNew();
                            try
                            {
                                if (isFileToCryp)
                                {
                                    timeForCryp = cryptoSoft.cryptoSoftEasySave("-c", $"{file}", $"{FileDestinationPath}");
                                    var test = timeForCryp;
                                }
                                else
                                {
                                    File.Copy(file, file.Replace(SourcePath, DestinationPath), true);
                                }

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

                            LogReturnCode += Log.LogFiles(LogArrayCreator(file, DestinationPath, FileTransferTime, timeForCryp));
                            ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model,"Active", TotalFilesToCopy, TotalFilesSizeToCopy, NbFilesLeft, FilesSizeLeft, file, DestinationPath));
                        };
                    }
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model,"Inactive", 0, 0, 0, 0, "", ""));
                }
                else
                {
                    ProgressStateReturnCode += ProgressState.ProgressStateFile(ProgressArrayCreator(model, "Inactive", 0, 0, 0, 0, "", ""));
                }
                if (LogReturnCode >= 1 || ProgressStateReturnCode >= 1)
                    return 1;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
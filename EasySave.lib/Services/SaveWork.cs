using EasySave.lib.Models;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

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
            string IsActive = "Active";
            int TotalFilesToCopy = 0;
            long TotalFilesSize = 0;
            int NbFilesLeftToDo = 0;
            long FilesSizeLeftToDo = 0;

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

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(DestinationPath, fileName);

                        DateTime today = DateTime.Now;                                          // Utile
                        FileInfo fileInfo = new FileInfo(file);                                 // Utile
                        long fileSize = fileInfo.Length;                                        // Utile
                        TotalFilesSize += fileSize;
                        var stopwatch = Stopwatch.StartNew();                                   // Utile

                        File.Copy(file, file.Replace(SourcePath, DestinationPath), true);

                        stopwatch.Stop();                                                       // Utile
                        double fileTransferTime = stopwatch.Elapsed.TotalSeconds;               // Utile
                        NbFilesLeftToDo++;
                        FilesSizeLeftToDo += fileSize;

                        string[] LogArray = new string[] {
                            fileName,
                            file,
                            destFile,
                            DestinationPath,
                            $"{fileSize}",
                            $"{fileTransferTime}",
                            today.ToString("MM/dd/yyyy hh:mm:ss")
                        };
                        Log.LogFiles(LogArray);

                        appelEtat(IsActive, TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, FilesSizeLeftToDo, $"{file}", $"{destFile}");
                    }
                    IsActive = "END";
                    Console.WriteLine("test d'appel END !!! After foreach");
                    Console.ReadKey();
                    appelEtat(IsActive,TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, FilesSizeLeftToDo);
                    return 0;
                }
                else
                {
                    IsActive = "END";
                    Console.WriteLine("test d'appel END !!!");
                    Console.ReadKey();
                    appelEtat(IsActive, TotalFilesToCopy, TotalFilesSize, NbFilesLeftToDo, FilesSizeLeftToDo);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                 //A retirer après test
                return 1;
            }
        }

        private int DifferentialCopyFiles()                                                     //string _SaveWorkModel.SourcePathSaveWork, string _SaveWorkModel.DestinationPathSaveWork
        {
            string SourcePath = _SaveWorkModel.SourcePathSaveWork;
            string DestinationPath = _SaveWorkModel.DestinationPathSaveWork;
            try
            {
                if (Directory.Exists(_SaveWorkModel.SourcePathSaveWork))
                {
                    foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
                    }
                    foreach (string file in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
                    {
                        if (File.GetLastWriteTime(file) > File.GetLastWriteTime(file.Replace(SourcePath, DestinationPath)))
                        {
                            string fileName = Path.GetFileName(file);
                            string destFile = Path.Combine(DestinationPath, fileName);
                            FileInfo fileInfo = new FileInfo(file);
                            long fileSize = fileInfo.Length;
                            DateTime today = DateTime.Now;

                            var stopwatch = Stopwatch.StartNew();

                            File.Copy(file, file.Replace(SourcePath, DestinationPath), true);

                            stopwatch.Stop();
                            double fileTransferTime = stopwatch.Elapsed.TotalSeconds;
                            
                            string[] LogArray = new string[] {
                            fileName,
                            file,
                            destFile,
                            DestinationPath,
                            $"{fileSize}",
                            $"{fileTransferTime}",
                            today.ToString("MM/dd/yyyy hh:mm:ss")
                            };
                            Log.LogFiles(LogArray);
                        }
                    }
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                                             //A retirer après test
                return 1;
            }
        }

        private int appelEtat(string IsActive,int TotalFilesToCopy,long TotalFilesSize,int NbFilesLeftToDo, long FilesSizeLeftToDo, string file = "0", string destFile = "0")
        {
           
            string[] ProgressArray = new string[] {
                        _SaveWorkModel.NameSaveWork,
                        file,
                        destFile,
                        DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"),
                        IsActive,
                        $"{TotalFilesToCopy}",
                        $"{TotalFilesSize}",
                        $"{NbFilesLeftToDo}",
                        $"{FilesSizeLeftToDo}"
                        };
            Etat.EtatFile(ProgressArray);

            return 0;
        }
    }
}
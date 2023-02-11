using EasySave.lib.Models;
using System.Diagnostics;
using System.IO;
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

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (!Directory.Exists(DestinationPath))
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    string[] files = Directory.GetFiles(SourcePath);

                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(DestinationPath, fileName);

                        DateTime today = DateTime.Now;                                          // Utile
                        FileInfo fileInfo = new FileInfo(file);                                 // Utile
                        long fileSize = fileInfo.Length;                                        // Utile
                        var stopwatch = Stopwatch.StartNew();                                   // Utile

                        File.Copy(file, destFile, true);        
                        
                        stopwatch.Stop();                                                       // Utile
                        double fileTransferTime = stopwatch.Elapsed.TotalSeconds;               // Utile

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

                    return 0;
                }
                else
                {
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
            try
            {
                if (Directory.Exists(_SaveWorkModel.SourcePathSaveWork))
                {
                    if (!Directory.Exists(_SaveWorkModel.DestinationPathSaveWork))
                    {
                        Directory.CreateDirectory(_SaveWorkModel.DestinationPathSaveWork);

                        string[] files = Directory.GetFiles(_SaveWorkModel.SourcePathSaveWork);

                        foreach (string file in files)
                        {
                            string fileName = Path.GetFileName(file);
                            string destFile = Path.Combine(_SaveWorkModel.DestinationPathSaveWork, fileName);
                            FileInfo fileInfo = new FileInfo(file);
                            long fileSize = fileInfo.Length;

                            var stopwatch = Stopwatch.StartNew();
                            File.Copy(file, destFile, true);
                            stopwatch.Stop();
                            double fileTransferTime = stopwatch.Elapsed.TotalSeconds;
                        }
                    }
                    else                                                                        // A modifier pour les logs !!!!
                    {
                        string[] files = Directory.GetFiles(_SaveWorkModel.SourcePathSaveWork);
                        foreach (string file in files)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            string destinationFile = Path.Combine(_SaveWorkModel.DestinationPathSaveWork, fileInfo.Name);

                            if (File.Exists(destinationFile))
                            {
                                FileInfo destinationFileInfo = new FileInfo(destinationFile);
                                if (destinationFileInfo.LastWriteTime < fileInfo.LastWriteTime)
                                {
                                    File.Copy(file, destinationFile, true);
                                    Console.WriteLine($"Le fichier {fileInfo.Name} a été copié avec succès.");              //A retirer après test
                                }
                                else
                                {
                                    Console.WriteLine($"Le fichier {fileInfo.Name} existe déjà et est plus récent.");       //A retirer après test
                                }
                            }
                            else
                            {
                                File.Copy(file, destinationFile);
                                Console.WriteLine($"Le fichier {fileInfo.Name} a été copié avec succès.");                  //A retirer après test
                            }
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
    }
}
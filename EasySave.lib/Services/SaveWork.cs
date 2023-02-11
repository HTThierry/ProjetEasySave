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

        static int CompleteCopyFilesFromDirToDir(string sourcePath, string destinationPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    string[] files = Directory.GetFiles(sourcePath);

                    foreach (string file in files)
                    {
                        DateTime today = DateTime.Now;
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destinationPath, fileName);
                        FileInfo fileInfo = new FileInfo(file);                               //Files size
                        long fileSize = fileInfo.Length;    

                        var stopwatch = Stopwatch.StartNew();                                 //start of the stopwatch
                        File.Copy(file, destFile, true);                                      
                        stopwatch.Stop();                                                     // stop it
                        double fileTransferTime = stopwatch.Elapsed.TotalSeconds;             //get the time in second

                        string[] LogArray = new string[] {
                            fileName,
                            file,
                            destFile,
                            destinationPath,
                            $"{fileSize}",
                            $"{fileTransferTime}",
                            today.ToString("MM/dd/yyyy hh:mm:ss")
                        };
                        //Append of information to the logs
                        log.logFiles(LogArray);
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

        static int DifferentialCopyFilesFromDirToDir(string sourcePath, string destinationPath)
        {
            try
            {
                if (Directory.Exists(sourcePath))
                {
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);

                        string[] files = Directory.GetFiles(sourcePath);

                        foreach (string file in files)
                        {
                            string fileName = Path.GetFileName(file);
                            string destFile = Path.Combine(destinationPath, fileName);
                            FileInfo fileInfo = new FileInfo(file);
                            long fileSize = fileInfo.Length;

                            var stopwatch = Stopwatch.StartNew();
                            File.Copy(file, destFile, true);
                            stopwatch.Stop();
                            double fileTransferTime = stopwatch.Elapsed.TotalSeconds;
                        }
                    }
                    else // A modifier pour les logs !!!!
                    {
                        string[] files = Directory.GetFiles(sourcePath);
                        foreach (string file in files)
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            string destinationFile = Path.Combine(destinationPath, fileInfo.Name);

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
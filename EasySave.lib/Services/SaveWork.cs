using EasySave.lib.Models;
using System.IO;

namespace EasySave.lib.Services
{
    public class SaveWork
    {
        public SaveWorkModel _SaveWorkModel = new SaveWorkModel();

        public string[] GetClassInfo()
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
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(destinationPath, fileName);
                        File.Copy(file, destFile, true);
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
                            File.Copy(file, destFile, true);
                        }
                    }
                    else
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
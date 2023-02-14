using EasySave.lib.Models;
using System.Configuration;
using System.Text.Json;
using System.Configuration;

namespace EasySave.lib.Services
{
    public class SaveWorkManager
    {
        public static int index;
        public SaveWork SaveWorkCreator(string[] AttributsForSaveWork)
        {
            SaveWork _SaveWork = new SaveWork();

            _SaveWork._SaveWorkModel.NameSaveWork = AttributsForSaveWork[0];
            _SaveWork._SaveWorkModel.TypeSaveWork = Int32.Parse(AttributsForSaveWork[1]);
            _SaveWork._SaveWorkModel.SourcePathSaveWork = AttributsForSaveWork[2];
            _SaveWork._SaveWorkModel.DestinationPathSaveWork = AttributsForSaveWork[3];

            return _SaveWork;
        }
        public int AddNewSaveWork(string[] AttributsForSaveWork, List<SaveWork> ArrayOfSaveWork)
        {
            if (ArrayOfSaveWork.Count < 5)
            {
                SaveWork _SaveWorkToSave = SaveWorkCreator(AttributsForSaveWork);
                ArrayOfSaveWork.Add(_SaveWorkToSave);
                ProgressState.AddNewSaveWorkProgressState(AttributsForSaveWork[0]);

                string jsonString = JsonSerializer.Serialize(_SaveWorkToSave._SaveWorkModel);
                string DirectoryPath = ConfigurationManager.AppSettings["SaveWorkPath"];
                string path = Path.Combine(DirectoryPath, $"{AttributsForSaveWork[0]}.json");

                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }

                try
                {
                    File.WriteAllText(path, jsonString);
                    return 0;
                }
                catch
                {
                    return 1;
                }

            }
            else
            {
                return 1;
            }
        }

        public int RemoveSaveWork(string SaveWorkID, List<SaveWork> ArrayOfSaveWork)
        {
            string directory = ConfigurationManager.AppSettings["SaveWorkPath"];
            string path = Path.Combine(directory, $"{ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].GetInstanceInfo()[0]}.json");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    ProgressState.RemoveSaveWork(ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1]._SaveWorkModel.NameSaveWork);
                    ArrayOfSaveWork.RemoveAt(Int32.Parse(SaveWorkID) - 1);
                    return 0;
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                                             //A retirer après test
                return 1;
            }
        }
        
        public int RemoveSaveWorkWPF(SaveWork _SaveWork, List<SaveWork> ArrayOfSaveWork)
        {
            string path = Path.Combine(ConfigurationManager.AppSettings["SaveWorkPath"], $"{_SaveWork.GetInstanceInfo()[0]}.json");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);

                    string NameOfSaveWork = _SaveWork._SaveWorkModel.NameSaveWork;
                    for (int i=0; i<ArrayOfSaveWork.Count; i++)
                    {
                        if (ArrayOfSaveWork[i]._SaveWorkModel.NameSaveWork == _SaveWork._SaveWorkModel.NameSaveWork)
                        {
                            index = i;
                        }
                    }
                    ArrayOfSaveWork.RemoveAt(index);
                    return 0;
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int ExecuteSaveWork(string SaveWorkID, List<SaveWork> ArrayOfSaveWork)
        {
            return ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].LaunchSaveWork();
        }
        public int ExecuteSaveWorkWPF(SaveWork _SaveWork)
        {
            return _SaveWork.LaunchSaveWork();
        }

        public int SequentialSaveWorksExecution(List<SaveWork> ArrayOfSaveWork)
        {
            int ReturnCode = 0;
            for (int i = 1; i < (ArrayOfSaveWork.Count + 1); i++)
            {
                if (ExecuteSaveWork($"{i}", ArrayOfSaveWork) == 1)
                {
                    ReturnCode++;
                }
            }
            if (ReturnCode > 0)
                return 1;
            else
                return 0;
        }
    }
}
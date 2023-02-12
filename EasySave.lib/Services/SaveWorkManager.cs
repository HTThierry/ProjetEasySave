using EasySave.lib.Models;
using System.Text.Json;

namespace EasySave.lib.Services
{
    public class SaveWorkManager
    {
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
                Etat.AddNewSaveWorkEtat(AttributsForSaveWork[0]);

                string jsonString = JsonSerializer.Serialize(_SaveWorkToSave._SaveWorkModel);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks", $"{AttributsForSaveWork[0]}.json");
                File.WriteAllText(path, jsonString);

                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int RemoveSaveWork(string SaveWorkID, List<SaveWork> ArrayOfSaveWork)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks", $"{ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].GetInstanceInfo()[0]}.json");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
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

        public int ExecuteSaveWork(string SaveWorkID, List<SaveWork> ArrayOfSaveWork)
        {
            return ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].LaunchSaveWork();
        }

        public int SequentialSaveWorksExecution(List<SaveWork> ArrayOfSaveWork)
        {
            for (int i = 1; i < (ArrayOfSaveWork.Count + 1); i++)
            {
                if (ExecuteSaveWork($"{i}", ArrayOfSaveWork) == 1)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
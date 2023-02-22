using EasySave.lib.Models;
using System.Configuration;
using System.Text.Json;

namespace EasySave.lib.Services
{
    public class SaveWorkManager
    {
        public static int index;
        public List<SaveWorkModel> ArrayOfSaveWork = new List<SaveWorkModel>();
        private SaveWorkService service = new SaveWorkService();

        public int AddNewSaveWork(SaveWorkModel model)
        {
            ArrayOfSaveWork.Add(model);
            ProgressStateService.AddNewSaveWorkProgressState(model.NameSaveWork);

            string jsonString = JsonSerializer.Serialize(model);
            string DirectoryPath = ConfigurationManager.AppSettings["SaveWorkPath"];
            string path = Path.Combine(DirectoryPath, $"{model.NameSaveWork}.json");

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

        public int SaveWorkInitializing()
        {
            string SaveWorkPath = ConfigurationManager.AppSettings["SaveWorkPath"];

            if (!Directory.Exists(SaveWorkPath))
            {
                Directory.CreateDirectory(SaveWorkPath);
                return 0;
            }

            string[] Files = Directory.GetFiles(SaveWorkPath, "*.json");
            int FileCount = Files.Length;

            for (int i = 0; i < FileCount; i++)
            {
                string Json = File.ReadAllText(Path.Combine(SaveWorkPath, Files[i]));
                SaveWorkModel _SaveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(Json)!;

                string[] AttributsForSaveWork = new string[4] { _SaveWorkJSON.NameSaveWork, $"{_SaveWorkJSON.TypeSaveWork}", _SaveWorkJSON.SourcePathSaveWork, _SaveWorkJSON.DestinationPathSaveWork };
                SaveWorkModel model = new SaveWorkModel()
                {
                    NameSaveWork = _SaveWorkJSON.NameSaveWork,
                    TypeSaveWork = _SaveWorkJSON.TypeSaveWork,
                    SourcePathSaveWork = _SaveWorkJSON.SourcePathSaveWork,
                    DestinationPathSaveWork = _SaveWorkJSON.DestinationPathSaveWork
                }; // crée savework à partir de attritub (atab des tring)
                ArrayOfSaveWork.Add(model);
                ProgressStateService.AddNewSaveWorkProgressState(_SaveWorkJSON.NameSaveWork);
            }

            return 0;
        }

        //public int RemoveSaveWork(string SaveWorkID)
        //{
        //    string directory = ConfigurationManager.AppSettings["SaveWorkPath"];
        //    string path = Path.Combine(directory, $"{ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].GetInstanceInfo()[0]}.json");
        //    try
        //    {
        //        if (File.Exists(path))
        //        {
        //            File.Delete(path);
        //            ProgressState.RemoveSaveWork(ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1]._SaveWorkModel.NameSaveWork);
        //            ArrayOfSaveWork.RemoveAt(Int32.Parse(SaveWorkID) - 1);
        //            return 0;
        //        }
        //        else
        //            return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Une erreur s'est produite : " + ex.Message);                                             //A retirer après test
        //        return 1;
        //    }
        //}

        public int RemoveSaveWorkWPF(SaveWorkModel model)
        {
            string path = Path.Combine(ConfigurationManager.AppSettings["SaveWorkPath"], $"{model.NameSaveWork}.json");
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);

                    string NameOfSaveWork = model.NameSaveWork;
                    for (int i = 0; i < ArrayOfSaveWork.Count; i++)
                    {
                        if (ArrayOfSaveWork[i].NameSaveWork == model.NameSaveWork)
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

        //public int ExecuteSaveWork(string SaveWorkID, List<SaveWorkService> ArrayOfSaveWork)
        //{
        //    return ArrayOfSaveWork[Int32.Parse(SaveWorkID) - 1].LaunchSaveWork();
        //}
        public int ExecuteSaveWorkWPF(SaveWorkModel model)
        {
            return service.LaunchSaveWork(model);
        }

        //public int SequentialSaveWorksExecution(List<SaveWorkService> ArrayOfSaveWork)
        //{
        //    int ReturnCode = 0;
        //    for (int i = 1; i < (ArrayOfSaveWork.Count + 1); i++)
        //    {
        //        if (ExecuteSaveWork($"{i}", ArrayOfSaveWork) == 1)
        //        {
        //            ReturnCode++;
        //        }
        //    }
        //    if (ReturnCode > 0)
        //        return 1;
        //    else
        //        return 0;
        //}

        private char[] IllegalChars = Path.GetInvalidPathChars();
        //private Model _Model = Model.GetInstance();

        public int TestNameSaveWork(string SaveWorkName)
        {
            if (SaveWorkName != "")
            {
                foreach (char c in IllegalChars)
                {
                    if (SaveWorkName.Contains(c))
                        return 1;
                }
                foreach (var index in ArrayOfSaveWork)
                {
                    if (index.NameSaveWork == SaveWorkName)
                    {
                        return 1;
                    }
                }
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int TestTypeSaveWork(string SaveWorkTypeToConvert)
        {
            int SaveWorkType;

            if (int.TryParse(SaveWorkTypeToConvert, out SaveWorkType))
            {
                if (SaveWorkType > 0 && SaveWorkType <= 2)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }

        public int TestSourcePathSaveWork(string SaveWorkSourcePath)
        {
            if (Directory.Exists(SaveWorkSourcePath))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int TestDestinationPathSaveWork(string SaveWorkDestinationPath)
        {
            if (SaveWorkDestinationPath != "")
            {
                foreach (char c in IllegalChars)
                {
                    if (SaveWorkDestinationPath.Contains(c))
                        return 1;
                }
                return 0;
            }
            else
                return 1;
        }
    }
}
using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Text.Json;

namespace EasySave.consoleApp.ViewModels
{
    public class ViewModel
    {
        public Model _Model = new Model();

        public static SaveWork SaveWorkCreator(string[] AttributsForSaveWork)
        {
            SaveWork _SaveWork = new SaveWork();

            _SaveWork._SaveWorkModel.NameSaveWork = AttributsForSaveWork[0];
            _SaveWork._SaveWorkModel.TypeSaveWork = Int32.Parse(AttributsForSaveWork[1]);
            _SaveWork._SaveWorkModel.SourcePathSaveWork = AttributsForSaveWork[2];
            _SaveWork._SaveWorkModel.DestinationPathSaveWork = AttributsForSaveWork[3];

            return _SaveWork;
        }

        public int UserChoiceTraitement(string UserChoice)
        {
            int SelectedOption;

            if (int.TryParse(UserChoice, out SelectedOption))
            {
                if (SelectedOption > 0 && SelectedOption <= 5)
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

        public int UserConfirmationTraitement(string UserKey)
        {
            if (UserKey.ToUpper() == "Y")
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int TestNameSaveWork(string SaveWorkName)
        {
            if (SaveWorkName != "")
            {
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
            if (SaveWorkDestinationPath == "")
            {
                return 1;
            }

            if (!Directory.Exists(SaveWorkDestinationPath))
            {
                Directory.CreateDirectory(SaveWorkDestinationPath);
            }

            return 0;
        }

        /// <summary>
        /// Adds a SaveWork as an instance and saves it in a JSON file
        /// </summary>
        /// <param name="AttributsForSaveWork"></param>
        /// <returns></returns>
        public int AddNewSaveWork(string[] AttributsForSaveWork)
        {
            if (_Model.ArrayOfSaveWork.Count < 5)
            {
                _Model.ArrayOfSaveWork.Add(SaveWorkCreator(AttributsForSaveWork));

                var SaveWorkJson = new SaveWorkModel
                {
                    NameSaveWork = AttributsForSaveWork[0],
                    TypeSaveWork = Int32.Parse(AttributsForSaveWork[1]),
                    SourcePathSaveWork = AttributsForSaveWork[2],
                    DestinationPathSaveWork = AttributsForSaveWork[3]
                };
                string jsonString = JsonSerializer.Serialize(SaveWorkJson);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks", $"{AttributsForSaveWork[0]}.json");
                File.WriteAllText(path, jsonString);

                return 0;
            }
            else
            {
                return 1;
            }
        }
        
        /// <summary>
        /// Instance all save works from the json files
        /// </summary>
        public int SaveWorkInstancing()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks");
            int filecount = Directory.GetFiles(path, "*.json").Length;
            string[] files = Directory.GetFiles(path, "*.json");
            string[] AttributsForPresentation = new string[4];
            if (filecount > 5)
                return 1;

            for (int i = 0; i < filecount; i++)
            {
                string json = File.ReadAllText(Path.Combine(path, files[i]));
                SaveWorkModel saveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(json)!;

                string[] AttributsForSaveWork = new string[4] { saveWorkJSON.NameSaveWork, $"{saveWorkJSON.TypeSaveWork}" , saveWorkJSON.SourcePathSaveWork, saveWorkJSON.DestinationPathSaveWork } ;

                _Model.ArrayOfSaveWork.Add(SaveWorkCreator(AttributsForSaveWork));
            }
            return 0;
        }

        public string[][] GetSaveWorkInfos()
        {
            string[][] SaveWorkInfos = new string[_Model.ArrayOfSaveWork.Count][];

            for (int i = 0; i < _Model.ArrayOfSaveWork.Count; ++i)
            {
                if (_Model.ArrayOfSaveWork[i] != null)
                    SaveWorkInfos[i] = _Model.ArrayOfSaveWork[i].GetInstanceInfo();
            }
            return SaveWorkInfos;
        }

        public string[] GetSaveWorkNames()
        {
            string[] NameOfSaveWorks = new string[_Model.ArrayOfSaveWork.Count];

            for (int i = 0; i < _Model.ArrayOfSaveWork.Count; i++)
            {
                NameOfSaveWorks[i] = _Model.ArrayOfSaveWork[i].GetInstanceInfo()[0];
            }
            return NameOfSaveWorks;
        }

        public int RemoveSaveWork(int SaveWorkID, string SaveWorkFilePath)
        {
            try
            {
                if (File.Exists(SaveWorkFilePath))
                {
                    File.Delete(SaveWorkFilePath);
                    _Model.ArrayOfSaveWork.RemoveAt(SaveWorkID);
                    return 0;
                }
                else
                    return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite : " + ex.Message);                                             //A retirer après test
                return 1;
            }
        }
    }
}
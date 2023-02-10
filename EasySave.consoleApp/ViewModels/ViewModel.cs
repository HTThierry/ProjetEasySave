using EasySave.lib.Models;
using EasySave.lib.Services;

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
            for (int i = 0; i < _Model.ArrayOfSaveWork.Length; i++)
            {
                if (_Model.ArrayOfSaveWork[i] == null)
                {
                    _Model.ArrayOfSaveWork[i] = SaveWorkCreator(AttributsForSaveWork);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks", $"{AttributsForSaveWork[0]}.json");
                    if (!File.Exists(path))
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine("{");
                            sw.WriteLine("    \"NameSaveWork\": \"" + AttributsForSaveWork[0] + "\",");
                            sw.WriteLine("    \"TypeSaveWork\": \"" + AttributsForSaveWork[1] + "\"");
                            sw.WriteLine("    \"SourcePathSaveWork\": \"" + AttributsForSaveWork[2] + "\",");
                            sw.WriteLine("    \"DestinationPathSaveWork\": \"" + AttributsForSaveWork[3] + "\",");
                            sw.WriteLine("}");
                        }
                    }
                    return 0;
                }
                if (i >= 5)
                    return 1;
            }
            return 1;
        }
        
        /// <summary>
        /// Instance all save works from the json files
        /// </summary>
        public void SaveWorkInstancing()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Services", "SaveWorks");
            int filecount = Directory.GetFiles(path, "*.json").Length;
            string[] files = Directory.GetFiles(path, "*.json");
            string[] AttributsForPresentation = new string[4];
            for (int i = 0; i < filecount; i++)
            {
                string[] lines = File.ReadAllLines(Path.Combine(path, files[i]));
                AttributsForPresentation[0] = lines[0];
                AttributsForPresentation[1] = lines[1];
                AttributsForPresentation[2] = lines[2];
                AttributsForPresentation[3] = lines[3];
                
                SaveWorkCreator(AttributsForPresentation);
            }
        }
    }
}
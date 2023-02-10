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

        public void AddNewSaveWork(string[] AttributsForSaveWork)
        {
            for (int i = 0; i < _Model.ArrayOfSaveWork.Length; i++)
            {
                if (_Model.ArrayOfSaveWork[i] == null)
                {
                    _Model.ArrayOfSaveWork[i] = SaveWorkCreator(AttributsForSaveWork);
                    break;
                }
                if (i >= 5)
                    break;

            }
        }

        public string[][] GetSaveWorkInfo()
        {
            string[][] SaveWorkInfos = new string[_Model.ArrayOfSaveWork.Length][];

            for (int i = 0; i < _Model.ArrayOfSaveWork.Length; ++i)
            {
                if (_Model.ArrayOfSaveWork[i] != null)
                    SaveWorkInfos[i] = _Model.ArrayOfSaveWork[i].GetInstanceInfo();
            }
            return SaveWorkInfos;
        }
    }
}
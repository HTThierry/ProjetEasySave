using EasySave.lib.Models;

namespace EasySave.lib.Services
{
    public class Overwatch
    {
        /// <summary>
        /// Refonte : Controleur
        /// Vérification des codes retours
        /// Controle des logiciels métiers
        /// Controle de l'existance de la clé
        /// Controle de l'existance de CryptoSoft
        /// ...
        /// </summary>


        private char[] IllegalChars = Path.GetInvalidPathChars();
        private Model _Model = Model.GetInstance();

        public int TestNameSaveWork(string SaveWorkName)
        {
            List<SaveWork> SaveWorkList = _Model.ArrayOfSaveWork;
            if (SaveWorkName != "")
            {
                foreach (char c in IllegalChars)
                {
                    if (SaveWorkName.Contains(c))
                        return 1;
                }
                foreach(SaveWork index in SaveWorkList)
                {
                    if (index._SaveWorkModel.NameSaveWork == SaveWorkName)
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
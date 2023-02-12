namespace EasySave.lib.Services
{
    public class EntryProcessingService
    {
        private char[] IllegalChars = Path.GetInvalidPathChars();

        public int TestNameSaveWork(string SaveWorkName)
        {
            if (SaveWorkName != "")
            {
                foreach (char c in IllegalChars)
                {
                    if (SaveWorkName.Contains(c))
                        return 1;
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
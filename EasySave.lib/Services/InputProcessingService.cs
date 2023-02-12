using EasySave.lib.Models;

namespace EasySave.lib.Services
{
    public class InputProcessingService
    {
        public int OptionSelectedTreatment(string UserChoice)
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

        public int ConfirmationTreatment(string UserKey)
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

        public int CheckSaveWorkIDTreatment(string UserSaveWorkID, int IdMax)
        {
            int SaveWorkID;

            if (int.TryParse(UserSaveWorkID, out SaveWorkID))
            {
                if (SaveWorkID > 0 && SaveWorkID <= IdMax)
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
    }
}
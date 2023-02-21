using EasySave.lib.Models;
namespace EasySave.lib.Services
{
    public class SaveWorkManager
    {
        private readonly List<SaveWorkModel> SaveWorks;

        public void SaveWorkCreate(SaveWorkModel model)
        {
            SaveWorks.Add(model);
        }
        public void SaveWorkDelete(SaveWorkModel model)
        {
            SaveWorks.Remove(model);
        }

    }
}
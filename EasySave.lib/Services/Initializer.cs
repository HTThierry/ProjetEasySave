using EasySave.lib.Models;
using System.Text.Json;
using System.Configuration;

namespace EasySave.lib.Services
{
    public class Initializer
    {
        private SaveWorkManager _SaveWorkManager = new SaveWorkManager();
        public int SaveWorkInitializing(List<SaveWork> ArrayOfSaveWork)
        {
            string SaveWorkPath = ConfigurationManager.AppSettings["SaveWorkPath"];

            if (!Directory.Exists(SaveWorkPath))
            {
                Directory.CreateDirectory(SaveWorkPath);
                return 0;
            }

            int FileCount = Directory.GetFiles(SaveWorkPath, "*.json").Length;
            string[] files = Directory.GetFiles(SaveWorkPath, "*.json");
            string[] AttributsForPresentation = new string[4];

            for (int i = 0; i < FileCount; i++)
            {
                string json = File.ReadAllText(Path.Combine(SaveWorkPath, files[i]));
                SaveWorkModel saveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(json)!;

                string[] AttributsForSaveWork = new string[4] { saveWorkJSON.NameSaveWork, $"{saveWorkJSON.TypeSaveWork}", saveWorkJSON.SourcePathSaveWork, saveWorkJSON.DestinationPathSaveWork };

                ArrayOfSaveWork.Add(_SaveWorkManager.SaveWorkCreator(AttributsForSaveWork));
                ProgressState.AddNewSaveWorkProgressState(AttributsForSaveWork[0]);
            }
            return 0;
        }
    }
}
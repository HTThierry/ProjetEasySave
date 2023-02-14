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
            string path = ConfigurationManager.AppSettings["SaveWorkPath"];

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            int filecount = Directory.GetFiles(path, "*.json").Length;
            string[] files = Directory.GetFiles(path, "*.json");
            string[] AttributsForPresentation = new string[4];

            for (int i = 0; i < filecount; i++)
            {
                string json = File.ReadAllText(Path.Combine(path, files[i]));
                SaveWorkModel saveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(json)!;

                string[] AttributsForSaveWork = new string[4] { saveWorkJSON.NameSaveWork, $"{saveWorkJSON.TypeSaveWork}", saveWorkJSON.SourcePathSaveWork, saveWorkJSON.DestinationPathSaveWork };

                ArrayOfSaveWork.Add(_SaveWorkManager.SaveWorkCreator(AttributsForSaveWork));
                ProgressState.AddNewSaveWorkProgressState(AttributsForSaveWork[0]);
            }
            return 0;
        }
    }
}
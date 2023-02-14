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

            string[] Files = Directory.GetFiles(SaveWorkPath, "*.json");
            int FileCount = Files.Length;

            for (int i = 0; i < FileCount; i++)
            {
                string Json = File.ReadAllText(Path.Combine(SaveWorkPath, Files[i]));
                SaveWorkModel _SaveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(Json)!;

                string[] AttributsForSaveWork = new string[4] { _SaveWorkJSON.NameSaveWork, $"{_SaveWorkJSON.TypeSaveWork}", _SaveWorkJSON.SourcePathSaveWork, _SaveWorkJSON.DestinationPathSaveWork };

                ArrayOfSaveWork.Add(_SaveWorkManager.SaveWorkCreator(AttributsForSaveWork));
                ProgressState.AddNewSaveWorkProgressState(_SaveWorkJSON.NameSaveWork);
            }
            return 0;
        }
    }
}
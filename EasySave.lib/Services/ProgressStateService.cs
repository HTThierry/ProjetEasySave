using EasySave.lib.Models;
using System.Configuration;
using System.Text.Json;

namespace EasySave.lib.Services
{
    public class ProgressStateService
    {
        public static List<ProgressStateModel> ProgressStates = new List<ProgressStateModel>();

        private static string CurrentName { get; set; } = "";

        private static int CurrentIndex { get; set; }

        public static void ProgressStateFile()
        {
            //Méthode temporairement vide -!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-!-

            //ProgressStateModel _ProgressStateModel = new ProgressStateModel
            //{
            //    Name = ProgressArray[0],
            //    Time = ProgressArray[1],
            //    ProgressState = ProgressArray[2],
            //    TotalFilesToCopy = ProgressArray[3],
            //    TotalFilesSizeToCopy = ProgressArray[4],
            //    NbFilesLeft = ProgressArray[5],
            //    FilesSizeLeft = ProgressArray[6],
            //    FilePath = ProgressArray[7],
            //    FileDestinationPath = ProgressArray[8]
            //};

            //if (CurrentName != ProgressArray[0])
            //{
            //    for (int i = 0; i < ProgressStates.Count; i++)
            //    {
            //        if (ProgressStates[i].Name == ProgressArray[0])
            //        {
            //            CurrentIndex = i;
            //        }
            //    }
            //}
            //CurrentName = ProgressArray[0];

            //ProgressStates[CurrentIndex] = _ProgressStateModel;

            //ProgressStates.FindIndex() = model;

            Serializer();
        }

        public static void AddNewSaveWorkProgressState(ProgressStateModel model)
        {
            //ProgressStateModel _ProgressStateModel = new ProgressStateModel
            //{
            //    Name = SaveWorkName,
            //    Time = today.ToString("dd/MM/yyyy hh:mm:ss"),
            //    ProgressState = "Inactive",
            //    TotalFilesToCopy = "",
            //    TotalFilesSizeToCopy = "",
            //    NbFilesLeft = "",
            //    FilesSizeLeft = "",
            //    FilePath = "",
            //    FileDestinationPath = ""
            //};

            ProgressStates.Add(model);

            Serializer();
        }

        public static void RemoveSaveWork(ProgressStateModel model)
        {
            ProgressStates.Remove(model);

            Serializer();
        }

        private static void Serializer()
        {
            string DirectoryPath = ConfigurationManager.AppSettings["ProgressStatePath"];
            string ProgressStatePath = Path.Combine(DirectoryPath, "ProgressState.json");

            Directory.CreateDirectory(DirectoryPath);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string JsonProgressState = JsonSerializer.Serialize(ProgressStates, options);

            try
            {
                File.WriteAllText(ProgressStatePath, JsonProgressState + Environment.NewLine);
            }
            catch { }
        }
    }
}
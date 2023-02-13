using System.IO;
using System.Text.Json;
using EasySave.lib.Models;

namespace EasySave.lib.Services
{
    public class ProgressState
    {
        public static List<ProgressStateModel> ProgressStates = new List<ProgressStateModel>();

        private static string CurrentName { get; set; } = "";

        private static int CurrentIndex { get; set; }

        public static int ProgressStateFile(string[] ProgressArray)
        {
            if (ProgressArray != null)
            {
                ProgressStateModel _ProgressStateModel = new ProgressStateModel
                {
                    Name = ProgressArray[0],
                    Time = ProgressArray[1],
                    ProgressState = ProgressArray[2],
                    TotalFilesToCopy = ProgressArray[3],
                    TotalFilesSizeToCopy = ProgressArray[4],
                    NbFilesLeft = ProgressArray[5],
                    FilesSizeLeft = ProgressArray[6],
                    FilePath = ProgressArray[7],
                    FileDestinationPath = ProgressArray[8]
                };

                if (CurrentName != ProgressArray[0])
                {
                    for (int i = 0; i < ProgressStates.Count; i++)
                    {
                        if (ProgressStates[i].Name == ProgressArray[0])
                        {
                            CurrentIndex = i;
                        }
                    }
                }
                CurrentName = ProgressArray[0];

                ProgressStates[CurrentIndex] = _ProgressStateModel;

                return Serializer();
            }
            else
            {
                return 1;
            }
        }
        public static int AddNewSaveWorkProgressState(string SaveWorkName)
        {
            DateTime today = DateTime.Now;

            ProgressStateModel _ProgressStateModel = new ProgressStateModel
            {
                Name = SaveWorkName,
                Time = today.ToString("dd/MM/yyyy hh:mm:ss"),
                ProgressState = "Inactive",
                TotalFilesToCopy = "",
                TotalFilesSizeToCopy = "",
                NbFilesLeft = "",
                FilesSizeLeft = "",
                FilePath = "",
                FileDestinationPath = ""
            };

            ProgressStates.Add(_ProgressStateModel);

            return Serializer();
        }

        public static int RemoveSaveWork(string SaveWorkName)
        {
            for (int i = 0; i < ProgressStates.Count; i++)
            {
                if (ProgressStates[i].Name == SaveWorkName)
                {
                    CurrentIndex = i;
                }
            }

            ProgressStates.RemoveAt(CurrentIndex);

            return Serializer();
        }

        private static int Serializer()
        {
            string DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "ProgressState");
            string ProgressStatePath = Path.Combine(DirectoryPath, "ProgressState.json");

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string JsonProgressState = JsonSerializer.Serialize(ProgressStates, options);

            try
            {
                File.WriteAllText(ProgressStatePath, JsonProgressState + Environment.NewLine);
                return 0;
            }
            catch
            {
                Console.WriteLine("Chiant 1");
                return 1;
            }
        }
    }
}
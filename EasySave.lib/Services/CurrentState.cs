using System.IO;
using System.Text.Json;
using EasySave.lib.Models;

namespace EasySave.lib.Services
{
    public class CurrentState
    {
        public static List<CurrentStateModel> CurrentStates = new List<CurrentStateModel>();

        private static string CurrentName { get; set; } = "";

        private static int CurrentIndex { get; set; }

        public static int CurrentStateFile(string[] ProgressArray)
        {
            if (ProgressArray != null)
            {
                CurrentStateModel _CurrentStateModel = new CurrentStateModel
                {
                    Name = ProgressArray[0],
                    Time = ProgressArray[1],
                    CurrentState = ProgressArray[2],
                    TotalFilesToCopy = ProgressArray[3],
                    TotalFilesSizeToCopy = ProgressArray[4],
                    NbFilesLeft = ProgressArray[5],
                    FilesSizeLeft = ProgressArray[6],
                    FilePath = ProgressArray[7],
                    FileDestinationPath = ProgressArray[8]
                };

                if (CurrentName != ProgressArray[0])
                {
                    for (int i = 0; i < CurrentStates.Count; i++)
                    {
                        if (CurrentStates[i].Name == ProgressArray[0])
                        {
                            CurrentIndex = i;
                        }
                    }
                }
                CurrentName = ProgressArray[0];

                CurrentStates[CurrentIndex] = _CurrentStateModel;

                return Serializer();
            }
            else
            {
                return 1;
            }
        }
        public static int AddNewSaveWorkCurrentState(string SaveWorkName)
        {
            DateTime today = DateTime.Now;

            CurrentStateModel _CurrentStateModel = new CurrentStateModel
            {
                Name = SaveWorkName,
                Time = today.ToString("dd/MM/yyyy hh:mm:ss"),
                CurrentState = "Inactive",
                TotalFilesToCopy = "",
                TotalFilesSizeToCopy = "",
                NbFilesLeft = "",
                FilesSizeLeft = "",
                FilePath = "",
                FileDestinationPath = ""
            };

            CurrentStates.Add(_CurrentStateModel);

            return Serializer();
        }

        public static int RemoveSaveWork(string SaveWorkName)
        {
            for (int i = 0; i < CurrentStates.Count; i++)
            {
                if (CurrentStates[i].Name == SaveWorkName)
                {
                    CurrentIndex = i;
                }
            }

            CurrentStates.RemoveAt(CurrentIndex);

            return Serializer();
        }

        private static int Serializer()
        {
            string DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "CurrentState");
            string CurrentStatePath = Path.Combine(DirectoryPath, "CurrentState.json");

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string JsonCurrentState = JsonSerializer.Serialize(CurrentStates, options);

            try
            {
                File.WriteAllText(CurrentStatePath, JsonCurrentState + Environment.NewLine);
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
using System.IO;
using System.Text.Json;
using EasySave.lib.Models;

namespace EasySave.lib.Services
{
    public class Etat
    {
        public static List<EtatModel> Etats = new List<EtatModel>();

        private static string CurrentName { get; set; } = "";

        private static int CurrentIndex { get; set; }

        public static int EtatFile(string[] ProgressArray)
        {
            if (ProgressArray != null)
            {
                EtatModel _EtatModel = new EtatModel
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
                    for (int i = 0; i < Etats.Count; i++)
                    {
                        if (Etats[i].Name == ProgressArray[0])
                        {
                            CurrentIndex = i;
                        }
                    }
                }
                CurrentName = ProgressArray[0];

                Etats[CurrentIndex] = _EtatModel;

                return Serializer();
            }
            else
            {
                return 1;
            }
        }
        public static int AddNewSaveWorkEtat(string SaveWorkName)
        {
            DateTime today = DateTime.Now;

            EtatModel _EtatModel = new EtatModel
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

            Etats.Add(_EtatModel);

            return Serializer();
        }

        public static int RemoveSaveWork(string SaveWorkName)
        {
            for (int i = 0; i < Etats.Count; i++)
            {
                if (Etats[i].Name == SaveWorkName)
                {
                    CurrentIndex = i;
                }
            }

            Etats.RemoveAt(CurrentIndex);

            return Serializer();
        }

        private static int Serializer()
        {
            string DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Etat");
            string EtatPath = Path.Combine(DirectoryPath, "etat.json");

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string JsonEtat = JsonSerializer.Serialize(Etats, options);

            try
            {
                File.WriteAllText(EtatPath, JsonEtat + Environment.NewLine);
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
using System.Text.Json;

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
                Console.WriteLine($"{ProgressArray[0]}");

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

                return Serializer(Etats);
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
                Time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                CurrentState = "Inactive",
                TotalFilesToCopy = "",
                TotalFilesSizeToCopy = "",
                NbFilesLeft = "",
                FilesSizeLeft = "",
                FilePath = "",
                FileDestinationPath = ""
            };

            Etats.Add(_EtatModel);

            Serializer(Etats);

            return 0;
        }

        public static int RemoveSaveWork(string SaveWorkName)
        {
            return 0;
        }

        private static int Serializer(List<EtatModel> Etats)
        {
            string EtatPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Etat", "etat.json");

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
                return 1;
            }
        }
    }
}
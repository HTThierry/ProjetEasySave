using System.Text.Json;

namespace EasySave.lib.Services
{
    public class Etat
    {
        public static int EtatFile(string[] ProgressArray)
        {
            if (ProgressArray != null)
            {
                List<EtatModel> Etats = new List<EtatModel>();
                string EtatPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Etat", "etat.json");

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

                Etats.Add(_EtatModel);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    //IgnoreNullValues = true
                };
                string JsonEtat = JsonSerializer.Serialize(Etats, options);

                try
                {
                    File.WriteAllText(EtatPath, JsonEtat + Environment.NewLine);
                }
                catch
                {
                    return 1;
                }



                //string json = File.ReadAllText(EtatPath);


                // Sa pète ici à partir du moment ou y a 2 fichiers dans etat.json, Quand y a un fichier dans etat.json on passe la condition et ça affiche test modif

                //List<EtatModel> EtatsVerif = JsonSerializer.Deserialize<List<EtatModel>>(json, options);

                //EtatModel etatTest = EtatsVerif.FirstOrDefault(name => name.Name == $"{ProgressArray[0]}");

                /*
                if (etatTest == null)
                {
                    string JsonLog = JsonSerializer.Serialize(Etats, options);
                    File.AppendAllText(EtatPath, JsonLog + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("test modif");
                    etatTest.Name = ProgressArray[0];
                    etatTest.SourceFilePath = ProgressArray[1];
                    etatTest.TargetFilePath = ProgressArray[2];
                    etatTest.Time = ProgressArray[3];
                    etatTest.IsActive = ProgressArray[4];
                    etatTest.TotalFilesToCopy = ProgressArray[5];
                    etatTest.TotalFilesSize = ProgressArray[6];
                    etatTest.NbFilesLeftToDo = ProgressArray[7];
                    etatTest.FilesSizeLeftToDo = ProgressArray[8];
                }
                */
                return 0;
            }
            else
            {
                Console.WriteLine("Merde1...");
                Console.ReadLine();
                return 1;
            }
        }
    }
}
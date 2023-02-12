using EasySave.lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EasySave.lib.Services
{
    public static class Etat
    {
        public static int EtatFile(string[] EtatArray)
        {
            if(EtatArray != null)
            {
                List<EtatEntry> Etats = new List<EtatEntry>();
                string EtatPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "EasySave.lib", "Etat", "etat.json");
                EtatEntry EtatEntry = new EtatEntry
                {
                    Name = EtatArray[0],
                    SourceFilePath = EtatArray[1],
                    TargetFilePath = EtatArray[2],
                    Time = EtatArray[3],
                    IsActive = EtatArray[4],
                    TotalFilesToCopy = EtatArray[5],
                    TotalFilesSize = EtatArray[6],
                    NbFilesLeftToDo = EtatArray[7],
                    FilesSizeLeftToDo = EtatArray[8]
                };
                Etats.Add(EtatEntry);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = File.ReadAllText(EtatPath);

                List<EtatEntry> EtatsVerif = JsonSerializer.Deserialize<List<EtatEntry>>(json);
                //Console.WriteLine(EtatArray[4]);

                EtatEntry etatTest = EtatsVerif.FirstOrDefault(Etat => Etat.Name == EtatArray[0]);

                if (etatTest == null)
                {
                    string JsonLog = JsonSerializer.Serialize(Etats, options);
                    File.AppendAllText(EtatPath, JsonLog + Environment.NewLine);
                }
                else
                {
                    etatTest.Name = EtatArray[0];
                    etatTest.SourceFilePath = EtatArray[1];
                    etatTest.TargetFilePath = EtatArray[2];
                    etatTest.Time = EtatArray[3];
                    etatTest.IsActive = EtatArray[4];
                    etatTest.TotalFilesToCopy = EtatArray[5];
                    etatTest.TotalFilesSize = EtatArray[6];
                    etatTest.NbFilesLeftToDo = EtatArray[7];
                    etatTest.FilesSizeLeftToDo = EtatArray[8];
                }
                //string JsonLog = JsonSerializer.Serialize(Etats, options);
                //File.WriteAllText(EtatPath, JsonLog + Environment.NewLine);
                return 0;
            }
            else
            {
                return 1;
            }

        }
    }
}
public class EtatEntry
{
    public string Name { get; set; }
    public string SourceFilePath { get; set; }
    public string TargetFilePath { get; set; }
    public string Time { get; set; }
    public string IsActive { get; set; }
    public string TotalFilesToCopy { get; set; }
    public string TotalFilesSize { get; set; }
    public string NbFilesLeftToDo { get; set; }
    public string FilesSizeLeftToDo { get; set; }
}



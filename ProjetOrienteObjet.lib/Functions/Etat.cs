using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;

namespace ProjetEasySave.lib.Functions
{
    public class Etat
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string PathTo { get; set; }
        public float fichierNum = 0;
        public string PathFrom { get; set; }
        public bool SaveState = false;

        public void etat()
        {

            //Nom fichier
            string path = FullPath;
            Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Etat du travail de sauvegarde
            string state;

            //Nombre de fichiers restants
            fichierNum++;
            float totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
            float remainingFiles = totalFiles - fichierNum;

            //Taille des fichiers restants
            FileInfo size = new FileInfo(FullPath); //Resultat en Octet
            //float totalsizefiles = 

            //Adresse complète du fichier Source en cours de sauvegarde



            //Adresse complète du fichier de destination


            if (SaveState == true)
            {
                state = "Actif";
                var stateObject = new
                {
                    name = Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    state = state,
                    remaining_files = remainingFiles,
                    //remaining_size_files = totalFiles.Length - size.Length,
                    FileSource = FullPath,
                    FileTarget = PathTo,
                };
                string json = JsonConvert.SerializeObject(stateObject, Formatting.Indented);

                if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json")) //A changer avec le dossie on recupere les logs
                {
                    File.WriteAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json", json);
                }
                else
                {
                    File.AppendAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json", json);
                }
            }
            else
            {
                state = "Non actif";
                var stateObject = new
                {
                    nom = Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    etat = state,
                };
                string json = JsonConvert.SerializeObject(stateObject, Formatting.Indented);

                if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json")) //A changer avec le dossie on recupere les logs
                {
                    File.WriteAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json", json);
                }
                else
                {
                    File.AppendAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json", json);
                }
            }



            //Console.WriteLine($"Il y a {fichierNum} fichiers copiés sur {totalFiles}");
            //float result = (fichierNum / totalFiles) * 100;
            //Console.WriteLine((int)result + "%");
        }
    }
}
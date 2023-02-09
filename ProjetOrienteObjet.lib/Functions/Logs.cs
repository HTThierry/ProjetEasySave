using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjetEasySave.lib.Functions
{
    
    public class Logs
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string PathTo { get; set; }


        //Créer un fichier log
        public void logs()
        {
            //Nom fichier
            string path = FullPath;
            Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Taille fichier
            FileInfo size = new FileInfo(FullPath); //Resultat en Octet

            var test = new
            {
                nom = Name,
                FileSource = FullPath,
                FileTarget = PathTo,
                destPath = "",
                FileSize = size.Length,
                FileTransferTime = _viewModel.timer(),
                time = today.ToString("MM/dd/yyyy hh:mm:ss"),
            };
            string json = JsonConvert.SerializeObject(test, Formatting.Indented);

            if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\log.json")) //A changer avec le dossie on recupere les logs
            {
                File.WriteAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\log.json", json);
            }
            else
            {
                File.AppendAllText(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\log.json", json);
            }
        }
    }
}
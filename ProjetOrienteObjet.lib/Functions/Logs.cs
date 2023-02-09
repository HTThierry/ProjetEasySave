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
        public lib.ViewModels.ConsoleAppViewModel _viewModel = new();
        //Créer un fichier log
        public void logs()
        {
            //Nom fichier
            string path = _viewModel.FullPath;
            _viewModel.Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Taille fichier
            FileInfo size = new FileInfo(_viewModel.FullPath); //Resultat en Octet

            var test = new
            {
                nom = _viewModel.Name,
                FileSource = _viewModel.FullPath,
                FileTarget = _viewModel.PathTo,
                destPath = "",
                FileSize = size.Length,
                FileTransferTime = _viewModel.timer(),
                time = today.ToString("MM/dd/yyyy hh:mm:ss"),
            };
            string json = JsonConvert.SerializeObject(test, Formatting.Indented);

            if (!System.IO.File.Exists(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\test.json")) //A changer avec le dossie on recupere les logs
            {
                File.WriteAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\test.json", json);
            }
            else
            {
                File.AppendAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\test.json", json);
            }
        }
    }
}

/*using Newtonsoft.Json;

namespace ProjetEasySave.lib.Functions
{
    public class Etat
    {
        public lib.ViewModels.ConsoleAppViewModel _viewModel = new();

        public void etat()
        {
            //Nom fichier
            string path = _viewModel.FullPath;
            _viewModel.Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Etat du travail de sauvegarde
            string state;
            if (_viewModel.SaveState == true){
                state = "Actif";
            }
            else
            {
                state = "Non actif";
            }

            var test = new
            {
                nom = _viewModel.Name,
                time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                etat = state,
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
}*/

using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;

namespace ProjetEasySave.lib.Functions
{
    public class Etat
    {
        public lib.ViewModels.ConsoleAppViewModel _viewModel = new();
        public void etat()
        {


            //Nom fichier
            string path = _viewModel.FullPath;
            _viewModel.Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Etat du travail de sauvegarde
            string state;

            //Nombre de fichiers restants
            _viewModel.fichierNum++;
            float totalFiles = Directory.GetFiles(_viewModel.PathFrom, "*.*", SearchOption.AllDirectories).Length;
            float remainingFiles = totalFiles - _viewModel.fichierNum;

            //Taille des fichiers restants
            FileInfo size = new FileInfo(_viewModel.FullPath); //Resultat en Octet
            //float totalsizefiles = 

            //Adresse complète du fichier Source en cours de sauvegarde



            //Adresse complète du fichier de destination


            if (_viewModel.SaveState == true)
            {
                state = "Actif";
                var stateObject = new
                {
                    name = _viewModel.Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    state = state,
                    remaining_files = remainingFiles,
                    //remaining_size_files = totalFiles.Length - size.Length,
                    FileSource = _viewModel.FullPath,
                    FileTarget = _viewModel.PathTo,
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
                    nom = _viewModel.Name,
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
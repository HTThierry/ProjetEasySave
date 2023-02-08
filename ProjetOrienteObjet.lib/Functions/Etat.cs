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
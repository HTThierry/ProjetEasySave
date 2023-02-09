using Newtonsoft.Json;

namespace ProjetEasySave.lib.Functions
{
    public class Logs
    {
        public PropertiesModel _PropertiesModel = new();

        //Créer un fichier log
        public void logs()
        {
            //Nom fichier
            string path = _PropertiesModel.FullPath;
            _PropertiesModel.Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Taille fichier
            FileInfo size = new FileInfo(_PropertiesModel.FullPath); //Resultat en Octet

            var test = new
            {
                nom = _PropertiesModel.Name,
                FileSource = _PropertiesModel.FullPath,
                FileTarget = _PropertiesModel.PathTo,
                destPath = "",
                FileSize = size.Length,
                FileTransferTime = _PropertiesModel.Timer(),
                time = today.ToString("MM/dd/yyyy hh:mm:ss"),
            };
            string json = JsonConvert.SerializeObject(test, Formatting.Indented);

            if (!System.IO.File.Exists(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\log.json")) //A changer avec le dossie on recupere les logs
            {
                File.WriteAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\log.json", json);
            }
            else
            {
                File.AppendAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\log.json", json);
            }
        }
    }
}
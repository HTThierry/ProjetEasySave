using Newtonsoft.Json;

namespace ProjetEasySave.lib.Functions
{
    public class Etat
    {
        public PropertiesModel _PropertiesModel = new();

        public void etat()
        {

            //Nom fichier
            string path = _PropertiesModel.FullPath;
            _PropertiesModel.Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Etat du travail de sauvegarde
            string state;

            //Nombre de fichiers restants
            _PropertiesModel.fichierNum++;
            float totalFiles = Directory.GetFiles(_PropertiesModel.PathFrom, "*.*", SearchOption.AllDirectories).Length;
            float remainingFiles = totalFiles - _PropertiesModel.fichierNum;

            //Taille des fichiers restants
            FileInfo size = new FileInfo(_PropertiesModel.FullPath); //Resultat en Octet
            //float totalsizefiles = 

            //Adresse complète du fichier Source en cours de sauvegarde

            //Adresse complète du fichier de destination


            if (_PropertiesModel.SaveState == true)
            {
                state = "Actif";
                var stateObject = new
                {
                    name = _PropertiesModel.Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    state = state,
                    remaining_files = remainingFiles,
                    //remaining_size_files = totalFiles.Length - size.Length,
                    FileSource = _PropertiesModel.FullPath,
                    FileTarget = _PropertiesModel.PathTo,
                };
                string json = JsonConvert.SerializeObject(stateObject, Formatting.Indented);

                if (!System.IO.File.Exists(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json")) //A changer avec le dossie on recupere les logs
                {
                    File.WriteAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json", json);
                }
                else
                {
                    File.AppendAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json", json);
                }
            }
            else
            {
                state = "Non actif";
                var stateObject = new
                {
                    nom = _PropertiesModel.Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    etat = state,
                };
                string json = JsonConvert.SerializeObject(stateObject, Formatting.Indented);

                if (!System.IO.File.Exists(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json")) //A changer avec le dossie on recupere les logs
                {
                    File.WriteAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json", json);
                }
                else
                {
                    File.AppendAllText(@"C:\Users\Utilisateur\Documents\CESI\Prosit 2\edqgezgez\etat.json", json);
                }
            }

            //Console.WriteLine($"Il y a {fichierNum} fichiers copiés sur {totalFiles}");
            //float result = (fichierNum / totalFiles) * 100;
            //Console.WriteLine((int)result + "%");
        }
    }
}
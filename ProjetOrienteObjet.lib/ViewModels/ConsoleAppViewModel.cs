using ProjetEasySave.lib.Functions;

namespace ProjetEasySave.lib.ViewModels
{
    public class ConsoleAppViewModel
    {
        public Save _Save = new();
        public Etat _Etat = new();
        public Logs _Logs = new();
        public PropertiesModel _PropertiesModel = new();

        public string? PathFrom;
        public string? PathTo { get; set; }
        public string? Choix { get; set; }
        public string? Message { get; set; }

        public void Sauvegarde()
        {
            _PropertiesModel.PathFrom = PathFrom;
            switch (Choix)
            {
                case "1":
                    //Console.WriteLine("Sauvegarde Complète");
                    //Message = "\n Sauvegarde Complète";
                    _Save.Complete();
                    break;

                case "2":
                    //Console.WriteLine("Sauvegarde Incrementielle");
                    //Message = "\n Sauvegarde Incrementielle";
                    _Save.Incrementiel();
                    break;

                default:
                    //Console.WriteLine("Sauvegarde Complète");
                    //Message = "\n Sauvegarde Complète";
                    break;
            }
        }

        public void EnregistrerSave(string nom, string from, string to, string type)
        {
            var save = new lib.Functions.SaveManager(nom, from, to, type);
            save.Creator();
        }

        public void SupprimerSave(string nom)
        {
            lib.Functions.SaveManager.Supprimer(nom);
        }

        /*Console.Clear();
        Console.WriteLine($"Il y a {fichierNum} fichiers copiés sur {totalFiles}");
        float result = (fichierNum / totalFiles) * 100;
        Console.WriteLine((int)result + "%");*/

        public void AfficherSave()
        {
            lib.Functions.SaveManager.Afficher();
        }
    }
}
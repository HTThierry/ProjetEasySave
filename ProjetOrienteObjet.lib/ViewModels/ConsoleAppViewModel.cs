using Newtonsoft.Json;

//using ProjetEasySave.lib.Functions;

namespace ProjetEasySave.lib.ViewModels
{
    public class ConsoleAppViewModel
    {
        //public lib.Functions.Logs _Logs = new();
        public string PathFrom { get; set; }

        public string PathTo { get; set; }
        public string Choix { get; set; }
        public string[] Message { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public float fichierNum = 0;
        private DateTime TimerA;
        private DateTime TimerB;
        public bool SaveState = false;
        private lib.Functions.Etat _Etat = new();
        private lib.Functions.Logs _Logs = new();

        public void Sauvegarde()
        {
            switch (Choix)
            {
                case "1":
                    //Console.WriteLine("Sauvegarde Complète");
                    //Message = "\n Sauvegarde Complète";
                    Complete();
                    break;

                case "2":
                    //Console.WriteLine("Sauvegarde Incrementielle");
                    //Message = "\n Sauvegarde Incrementielle";
                    Incrementiel();
                    break;

                default:
                    //Console.WriteLine("Sauvegarde Complète");
                    //Message = "\n Sauvegarde Complète";
                    break;
            }
        }

        public void Complete()
        {
            if (System.IO.Directory.Exists(PathFrom) && System.IO.Directory.Exists(PathTo))
            {
                int fichierNum = 0;
                int totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
                foreach (string dirPath in Directory.GetDirectories(PathFrom, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(PathFrom, PathTo));
                }
                foreach (string newPath in Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories))
                {
                    SaveState = true;
                    TimerA = DateTime.Now;
                    FullPath = newPath;
                    File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                    fichierNum++;
                    TimerB = DateTime.Now;
                    _Logs.logs();
                    _Etat.etat();
                }
                //Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
            }
            timer();
        }

        public void Incrementiel()
        {
            int totalFiles = 0;
            int modifiedFiles = -1;
            if (System.IO.Directory.Exists(PathTo))
            {
                totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
            }
            if (System.IO.Directory.Exists(PathFrom) && System.IO.Directory.Exists(PathTo))
            {
                modifiedFiles = 0;
                foreach (string dirPath in Directory.GetDirectories(PathFrom, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(PathFrom, PathTo));
                }
                foreach (string newPath in Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories))
                {
                    if (File.GetLastWriteTime(newPath) > File.GetLastWriteTime(newPath.Replace(PathFrom, PathTo)))
                    {
                        SaveState = true;
                        TimerA = DateTime.Now;
                        FullPath = newPath;
                        File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                        modifiedFiles++;
                        TimerB = DateTime.Now;
                        _Logs.logs();
                        _Etat.etat();
                    }
                }
            }
            else
            {
                //Message = "Votre chemin initial ou de destination n'existe pas";
                Message = new string[] { "wrongPath" };
            }

            if (modifiedFiles == 0)
            {
                //Message = $"Copie terminée, vos fichiers sont à jour, aucun fichier n'a été modifié sur {totalFiles} analysés.";
                Message = new string[] { "noFiles", totalFiles.ToString() };
            }
            if (modifiedFiles > 0)
            {
                //Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {modifiedFiles} analysés.";
                Message = new string[] { "copyEnd", modifiedFiles.ToString(), modifiedFiles.ToString() };
            }
        }

        //Fonction qui calcule le temps entre chaque copie de fichier
        public TimeSpan timer()
        {
            TimeSpan time = TimerB.Subtract(TimerA);
            return time;
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
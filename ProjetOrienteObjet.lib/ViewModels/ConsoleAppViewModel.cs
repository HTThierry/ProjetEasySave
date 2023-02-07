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
        public string Message { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public float fichierNum = 0;
        private DateTime TimerA;
        private DateTime TimerB;

        public void Sauvegarde()
        {
            switch (Choix)
            {
                case "1":
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "\nSauvegarde Complète";
                    Complete();
                    break;

                case "2":
                    //Console.WriteLine("Sauvegarde Incrementielle");
                    Message = "\nSauvegarde Incrementielle";
                    Incrementiel();
                    break;

                default:
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "\nSauvegarde Complète";
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
                    TimerA = DateTime.Now;
                    FullPath = newPath;
                    File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                    fichierNum++;
                    TimerB = DateTime.Now;
                    logs();
                    etat();
                }
                Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
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
                        TimerA = DateTime.Now;
                        File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                        modifiedFiles++;
                        TimerB = DateTime.Now;
                        logs();
                        etat();
                    }
                }
            }
            else
            {
                Message = "Votre chemin initial ou de destination n'existe pas";
            }

            if (modifiedFiles == 0)
            {
                Message = $"Copie terminée, vos fichiers sont à jour, aucun fichier n'a été modifié sur {totalFiles} analysés.";
            }
            if (modifiedFiles > 0)
            {
                Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {totalFiles} analysés.";
            }
        }

        //Fonction qui calcule le temps entre chaque copie de fichier
        public TimeSpan timer()
        {
            TimeSpan time = TimerB.Subtract(TimerA);
            return time;
        }

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
                FileTransferTime = timer(),
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

        public void etat()
        {
            //Nom fichier
            string path = FullPath;
            Name = Path.GetFileName(path);

            //Date et heure
            DateTime today = DateTime.Now;

            //Etat du travail de sauvegarde
            bool SaveState = false;
            string state;

            //Nombre de fichiers restants
            fichierNum++;
            float totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
            float remainingFiles = totalFiles - fichierNum;

            //Taille des fichiers restants
            FileInfo size = new FileInfo(FullPath); //Resultat en Octet

            //Adresse complète du fichier Source en cours de sauvegarde



            //Adresse complète du fichier de destination


            if (SaveState == true)
            {
                state = "Actif";
                var stateObject = new
                {
                    nom = Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    etat = state,
                    //remainingFiles = totalFiles.Length - size.Length,
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



            Console.WriteLine($"Il y a {fichierNum} fichiers copiés sur {totalFiles}");
            float result = (fichierNum / totalFiles) * 100;
            Console.WriteLine((int)result + "%");
        }
    }
}
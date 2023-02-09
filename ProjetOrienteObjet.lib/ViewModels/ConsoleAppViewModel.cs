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

        public void Sauvegarde()
        {
            switch (Choix)
            {
                case "1":
                    //Message = "\nFull backup";
                    Complete();
                    break;

                case "2":
                    //Message = "\nIncremental Backup";
                    Incrementiel();
                    break;

                default:
                    //Message = "\nFull backup";
                    Complete();
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
                        //logs();
                        //etat();
                    }
                //Message = $"Copy completed, {fichierNum} files copied to {totalFiles} of {PathFrom} to {PathTo}.";
                Message = new string[] { "backupFull", fichierNum.ToString(), totalFiles.ToString(), PathFrom, PathTo };
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
                       // logs();
                        //etat();
                    }
                }
            }
            else
            {
                //Message = "Your starting or destination path does not exist.";
                Message = new string[] { "wrongPath" };
        }

            if (modifiedFiles == 0)
            {
                //Message = $"Copy completed, your files are up to date, no files have been modified on {totalFiles} analyzed.";
                Message = new string[] { "noFiles", totalFiles.ToString() };
        }
            if(modifiedFiles > 0)
            {
                //Message = $"Copy completed, {modifiedFiles} modified files to {modifiedFiles} analyzed..";
                Message = new string[] { "copyEnd", modifiedFiles.ToString(), modifiedFiles.ToString() };
        }
        }



         
        // Function that calculates the time between each file copy
        public TimeSpan timer()
        {
            TimeSpan time = TimerB.Subtract(TimerA);
            return time;
        }

        public void logs()
        {
            //File name
            string path = FullPath;
            Name = Path.GetFileName(path);

            //Date and time
            DateTime today = DateTime.Now;

            //File size
            FileInfo size = new FileInfo(FullPath); //Result in Byte

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

            if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\log.json")) //To change with the file we recover the logs
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
            //File name
            string path = FullPath;
            Name = Path.GetFileName(path);

            //Date and time
            DateTime today = DateTime.Now;

            //Status of the backup work
            string state;

            //Number of files remaining
            fichierNum++;
            float totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
            float remainingFiles = totalFiles - fichierNum;

            //Size of remaining files
            FileInfo size = new FileInfo(FullPath); //Result in Byte

            //Full address of the Source file being saved



            //Full address of the destination file


            if (SaveState == true)
            {
                state = "Actif";
                var stateObject = new
                {
                    name = Name,
                    time = today.ToString("MM/dd/yyyy hh:mm:ss"),
                    state = state,
                    remaining_files  = remainingFiles,
                    //remaining_size_files = totalFiles.Length - size.Length,
                    FileSource = FullPath,
                    FileTarget = PathTo,
                };
                string json = JsonConvert.SerializeObject(stateObject, Formatting.Indented);

                if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json")) //To change with the file we recover the logs
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

                if (!System.IO.File.Exists(@"C:\Users\peyo6\OneDrive\Bureau\GIGATEST\etat.json")) //To change with the file we recover the logs
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
        Console.WriteLine((int) result + "%");*/
        public void AfficherSave()
        {
            lib.Functions.SaveManager.Afficher();
        }

    }
}
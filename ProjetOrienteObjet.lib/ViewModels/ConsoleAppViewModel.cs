namespace ProjetEasySave.lib.ViewModels
{
    public class ConsoleAppViewModel
    {
        public lib.Functions.Logs _Logs = new();
        public string PathFrom { get; set; }
        public string PathTo { get; set; }
        public int Choix { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        private DateTime TimerA;
        private DateTime TimerB;

        public void Sauvegarde()
        {
            switch (Choix)
            {
                case 1:
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "\nSauvegarde Complète";
                    Complete();
                    break;

                case 2:
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
            if (System.IO.Directory.Exists(PathFrom))
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
                    _Logs.logs();
                }
                Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
            }
            timer();
        }

        public void Incrementiel()
        {
            int modifiedFiles = 0;
            int totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
            if (System.IO.Directory.Exists(PathFrom))
            {
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
                        _Logs.logs();
                    }
                }
            }
            if (modifiedFiles == 0)
            {
                Message = $"Copie terminée, aucun fichier n'a été modifié sur {totalFiles} analysés.";
            }
            else
            {
                Console.WriteLine(modifiedFiles + " fichier(s) modifié(s)");
                Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {totalFiles} analysés.";
            }
        }

        //Fonction qui calcule le temps entre chaque copie de fichier
        public TimeSpan timer()
        {
            TimeSpan time = TimerB.Subtract(TimerA);
            return time;
        }
    }
}
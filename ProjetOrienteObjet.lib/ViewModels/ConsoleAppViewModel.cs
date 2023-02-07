namespace ProjetEasySave.lib.ViewModels
{
    public class ConsoleAppViewModel
    {
        public string? PathFrom { get; set; }
        public string? PathTo { get; set; }
        public string? Choix { get; set; }
        public string? Message { get; set; }
        public int Langue { get; set; }
        public string? FullPath { get; set; }

        public void Sauvegarde()
        {
            if(Choix != "1" && Choix != "2")
            {
                Message = "Saisie incorrecte, veuillez choisir entre sauvegarde Complète et Incrémentielle";
            }
            else
            {
                switch (Choix)
                {
                    case "1":
                        //Console.WriteLine("Sauvegarde Complète");
                        Message = "Sauvegarde Complète";
                        Complete();
                        break;

                    case "2":
                        //Console.WriteLine("Sauvegarde Incrementielle");
                        Message = "Sauvegarde Incrementielle";
                        Incrementiel();
                        break;

                    default:
                        //Console.WriteLine("Sauvegarde Complète");
                        Message = "Sauvegarde Complète";
                        break;
                }

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
                    FullPath = newPath;
                    File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                    fichierNum++;
                }
                Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
            }
            else
            {
                Message = "Votre chemin initial ou de destination n'existe pas";
            }
        }

        public void Incrementiel()
        {
            int totalFiles = 0;
            int modifiedFiles = -1;
            if(System.IO.Directory.Exists(PathTo))
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
                        File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                        modifiedFiles++;
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
            if(modifiedFiles > 0)
            {
                Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {totalFiles} analysés.";
            }
        }
    }
}
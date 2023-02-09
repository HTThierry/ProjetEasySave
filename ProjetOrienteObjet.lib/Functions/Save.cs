namespace ProjetEasySave.lib.Functions
{
    internal class Save
    {
        public string? PathFrom { get; set; }
        public string? PathTo { get; set; }

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
                    DateTime timerA = DateTime.Now;
                    string fullPath = newPath;
                    File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                    fichierNum++;
                    DateTime timerB = DateTime.Now;
                }
            }
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
                        DateTime timerA = DateTime.Now;
                        File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                        modifiedFiles++;
                        DateTime timerB = DateTime.Now;
                    }
                }
            }
            else
            {
                //Message = "Votre chemin initial ou de destination n'existe pas";
            }

            if (modifiedFiles == 0)
            {
                //Message = $"Copie terminée, vos fichiers sont à jour, aucun fichier n'a été modifié sur {totalFiles} analysés.";
            }
            if (modifiedFiles > 0)
            {
                //Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {totalFiles} analysés.";
            }
        }
    }
}
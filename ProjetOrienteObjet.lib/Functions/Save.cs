using System.Diagnostics;

namespace ProjetEasySave.lib.Functions
{
    public class Save
    {
        public PropertiesModel _PropertiesModel = new();
        public Logs _Logs = new Logs();
        public Etat _Etat = new Etat();

        private Stopwatch TimerA = new Stopwatch();

        public void Complete()
        {
            if (System.IO.Directory.Exists(_PropertiesModel.PathFrom) && System.IO.Directory.Exists(_PropertiesModel.PathTo))
            {
                int totalFiles = Directory.GetFiles(_PropertiesModel.PathFrom, "*.*", SearchOption.AllDirectories).Length;
                foreach (string dirPath in Directory.GetDirectories(_PropertiesModel.PathFrom, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(_PropertiesModel.PathFrom, _PropertiesModel.PathTo));
                }
                foreach (string newPath in Directory.GetFiles(_PropertiesModel.PathFrom, "*.*", SearchOption.AllDirectories))
                {
                    _PropertiesModel.SaveState = true;
                    TimerA.Start();
                    _PropertiesModel.FullPath = newPath;
                    File.Copy(newPath, newPath.Replace(_PropertiesModel.PathFrom, _PropertiesModel.PathTo), true);
                    _PropertiesModel.fichierNum++;
                    TimerA.Stop();
                    _Logs.logs();
                    _Etat.etat();
                }
                //Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
            }
            _PropertiesModel.Timer();
        }

        public void Incrementiel()
        {
            int totalFiles = 0;
            int modifiedFiles = -1;
            if (System.IO.Directory.Exists(_PropertiesModel.PathTo))
            {
                totalFiles = Directory.GetFiles(_PropertiesModel.PathFrom, "*.*", SearchOption.AllDirectories).Length;
            }
            if (System.IO.Directory.Exists(_PropertiesModel.PathFrom) && System.IO.Directory.Exists(_PropertiesModel.PathTo))
            {
                modifiedFiles = 0;
                foreach (string dirPath in Directory.GetDirectories(_PropertiesModel.PathFrom, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(_PropertiesModel.PathFrom, _PropertiesModel.PathTo));
                }
                foreach (string newPath in Directory.GetFiles(_PropertiesModel.PathFrom, "*.*", SearchOption.AllDirectories))
                {
                    if (File.GetLastWriteTime(newPath) > File.GetLastWriteTime(newPath.Replace(_PropertiesModel.PathFrom, _PropertiesModel.PathTo)))
                    {
                        //SaveState = true;
                        TimerA.Start();
                        //FullPath = newPath;
                        File.Copy(newPath, newPath.Replace(_PropertiesModel.PathFrom, _PropertiesModel.PathTo), true);
                        modifiedFiles++;
                        TimerA.Start();
                        //_Logs.logs();
                        //_Etat.etat();
                    }
                }
                _PropertiesModel.Timer();
            }
            else
            {
                //Message = "Votre chemin initial ou de destination n'existe pas";
                //Message = new string[] { "wrongPath" };
            }

            if (modifiedFiles == 0)
            {
                //Message = $"Copie terminée, vos fichiers sont à jour, aucun fichier n'a été modifié sur {totalFiles} analysés.";
                //Message = new string[] { "noFiles", totalFiles.ToString() };
            }
            if (modifiedFiles > 0)
            {
                //Message = $"Copie terminée, {modifiedFiles} fichiers modifiés sur {modifiedFiles} analysés.";
                //Message = new string[] { "copyEnd", modifiedFiles.ToString(), modifiedFiles.ToString() };
            }
        }
    }
}
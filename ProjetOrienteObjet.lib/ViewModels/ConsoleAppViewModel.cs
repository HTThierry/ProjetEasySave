using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetEasySave.lib.ViewModels
{
    public class ConsoleAppViewModel
    {
        public string PathFrom { get; set; }
        public string PathTo { get; set; }
        public int Choix { get; set; }
        public string Message { get; set; }

        public void Sauvegarde()
        {
            switch (Choix)
            {
                case 1:
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "Sauvegarde Complète";
                    Complete();
                    break;
                case 2:
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

        public void Complete()
        {
            if (System.IO.Directory.Exists(PathFrom))
            {
                int fichierNum = 1;
                int totalFiles = Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories).Length;
                foreach (string dirPath in Directory.GetDirectories(PathFrom, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(PathFrom, PathTo));
                }
                foreach (string newPath in Directory.GetFiles(PathFrom, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                    fichierNum++;
                }
                Message = $"Copie terminée, {fichierNum} fichiers copiés sur {totalFiles} de {PathFrom} vers {PathTo}.";
            }
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
                        File.Copy(newPath, newPath.Replace(PathFrom, PathTo), true);
                        modifiedFiles++;
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
    }
}

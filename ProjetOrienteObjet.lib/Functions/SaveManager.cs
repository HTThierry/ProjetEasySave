using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetEasySave.lib.Functions
{
    internal class SaveManager
    {
        private Models.SaveManagerModel _SaveManagerModel = new();

        public void Create()
        {
            int filecount = Directory.GetFiles(@"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde").Length;
            if(filecount<5)
            {
                string path = @"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde\" + _SaveManagerModel.Name + ".json";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("{");
                        sw.WriteLine("    \"name\": \"" + _SaveManagerModel.Name + "\",");
                        sw.WriteLine("    \"from\": \"" + _SaveManagerModel.From + "\",");
                        sw.WriteLine("    \"to\": \"" + _SaveManagerModel.To + "\",");
                        sw.WriteLine("    \"save\": \"" + _SaveManagerModel.Save + "\"");
                        sw.WriteLine("}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Vous avez atteint le nombre maximum de sauvegardes"); //à changer pour l'afficher depuis la console
            }
        }

        public static void Delete(string nom)
        {
            string path = @"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde\" + nom + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void Show()
        {
            string path = @"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde\";
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (string file in files)
            {
                //print only the name of the file without the extension
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }
    }
}

namespace ProjetEasySave.lib.Functions
{
    internal class SaveManager
    {
        public string? Name { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Save { get; set; }

        public SaveManager(string name, string from, string to, string save)
        {
            Name = name;
            From = from;
            To = to;
            Save = save;
        }

        public void Creator()
        {
            int filecount = Directory.GetFiles(@"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde").Length;
            if (filecount < 5)
            {
                string path = @"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde\" + Name + ".json";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("{");
                        sw.WriteLine("    \"name\": \"" + Name + "\",");
                        sw.WriteLine("    \"from\": \"" + From + "\",");
                        sw.WriteLine("    \"to\": \"" + To + "\",");
                        sw.WriteLine("    \"save\": \"" + Save + "\"");
                        sw.WriteLine("}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Vous avez atteint le nombre maximum de sauvegardes"); //à changer pour l'afficher depuis la console
            }
        }

        public static void Supprimer(string nom)
        {
            string path = @"..\..\..\..\ProjetOrienteObjet.lib\Functions\travauxSauvegarde\" + nom + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void Afficher()
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetEasySave.consoleApp.Views
{
    internal class ConsoleAppViewTemp
    {
        private lib.ViewModels.ConsoleAppViewModel _ConsoleAppViewModel = new();
        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1 - Liste des travaux de sauvegarde :");
                Console.WriteLine("2 - Créer une sauvegarde :");
                Console.WriteLine("3 - Supprimer une sauvegarde :");
                int choix = Convert.ToInt32(Console.ReadLine());

                switch (choix)
                {
                    case 1:
                        string[] files = _ConsoleAppViewModel.ShowSave();
                        int id = 1;
                        foreach (string file in files)
                        {
                            //print only the name of the file without the extension
                            Console.WriteLine(id + " - " + Path.GetFileNameWithoutExtension(file));
                            id++;
                        }
                        Console.WriteLine("\n 1 - Lancer un travail de sauvegarde");
                        Console.WriteLine("2 - Lancer les 5 travaux");
                        choix = Convert.ToInt32(Console.ReadLine());
                        //lancer les travaux de sauvegarde ici (code inexistant)
                        break;
                    case 2:
                        Console.WriteLine("Nom de la sauvegarde :");
                        string? nom = Console.ReadLine();
                        Console.WriteLine("Chemin du dossier à sauvegarder :");
                        string? from = Console.ReadLine();
                        Console.WriteLine("Chemin du dossier de sauvegarde :");
                        string? to = Console.ReadLine();
                        Console.WriteLine("Type de sauvegarde :");
                        string? type = Console.ReadLine();
                        _ConsoleAppViewModel.EnregistrerSave(nom, from, to, type);
                        break;
                    case 3:
                        Console.WriteLine("Nom de la sauvegarde :");
                        string? nom2 = Console.ReadLine();
                        _ConsoleAppViewModel.SupprimerSave(nom2);
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
        }
    }
}

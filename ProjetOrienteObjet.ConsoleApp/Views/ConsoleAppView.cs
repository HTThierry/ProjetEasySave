using System.Globalization;
using Newtonsoft.Json;
using System;
using System.IO;
using ProjetEasySave.lib.ViewModels;

namespace ProjetEasySave.consoleApp.Views
{
    public class ConsoleAppView
    {
        private lib.ViewModels.ConsoleAppViewModel _viewModel = new();
        private lib.ViewModels.langue langue = new();

        public void Show()
        {
            string Message;
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.title);
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice1);
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice2);
            _viewModel.Choix = Console.ReadLine();

            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathSource);
            _viewModel.PathFrom = Console.ReadLine();

            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathDest);
            _viewModel.PathTo = Console.ReadLine();
            switch (_viewModel.Choix)
            {
                case "1":
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "\nSauvegarde Complète";
                    
                    break;

                case "2":
                    //Console.WriteLine("Sauvegarde Incrementielle");
                    Message = "\nSauvegarde Incrementielle";
                    
                    break;

                default:
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = "\nSauvegarde Complète";
                    break;
            }
            _viewModel.Sauvegarde();
            switch (_viewModel.Message[0])
            {
                case "backupFull":
                    Message = $"Copie terminée, {_viewModel.Message[1]} fichiers copiés sur {_viewModel.Message[2]} de {_viewModel.Message[3]} vers {_viewModel.Message[4]}.";
                    break;

                case "wrongPath":
                    Message = "Votre chemin initial ou de destination n'existe pas";
                    break;

                case "noFiles":
                    Message = $"Copie terminée, vos fichiers sont à jour, aucun fichier n'a été modifié sur {_viewModel.Message[1]} analysés.";
                    break;

                case "copyend":
                    Message = $"Copie terminée, {_viewModel.Message[1]} fichiers modifiés sur {_viewModel.Message[2]} analysés.";
                    break;
            }
            Console.WriteLine(Message);
        }
    }
}
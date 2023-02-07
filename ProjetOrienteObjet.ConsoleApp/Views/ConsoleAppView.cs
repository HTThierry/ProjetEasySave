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
            Console.WriteLine("type de sauvegarde :");
            Console.WriteLine("1 - Sauvegarde Complète (default)");
            Console.WriteLine("2 - Sauvegarde Incrementielle");
            _viewModel.Choix = Console.ReadLine();

            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathSource);
            _viewModel.PathFrom = Console.ReadLine();

            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathDest);
            _viewModel.PathTo = Console.ReadLine();

            _viewModel.Sauvegarde();

            Console.WriteLine(_viewModel.Message);
        }
    }
}
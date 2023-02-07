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

            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.title);
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice1);
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice2);
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
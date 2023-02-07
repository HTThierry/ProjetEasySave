﻿namespace ProjetEasySave.consoleApp.Views
{
    public class ConsoleAppView
    {
        private lib.ViewModels.ConsoleAppViewModel _viewModel = new();

        public void Show()
        {
            Console.WriteLine("type de sauvegarde :");
            Console.WriteLine("1 - Sauvegarde Complète (default)");
            Console.WriteLine("2 - Sauvegarde Incrementielle");
            _viewModel.Choix = Console.ReadLine();

            Console.WriteLine("chemin de sauvegarde initial :");
            _viewModel.PathFrom = Console.ReadLine();

            Console.WriteLine("chemin de sauvegarde final :");
            _viewModel.PathTo = Console.ReadLine();

            _viewModel.Sauvegarde();

            Console.WriteLine(_viewModel.Message);
        }
    }
}
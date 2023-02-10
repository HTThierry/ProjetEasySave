﻿using EasySave.consoleApp.Resources;
using EasySave.consoleApp.ViewModels;

namespace EasySave.consoleApp.Views
{
    public class View
    {
        public ViewModel _ViewModel = new ViewModel();

        public void Show()
        {
            if (_ViewModel.SaveWorkInstancing() == 1)
            {
                Console.WriteLine("The limit of SaveWork files has been exceeded.");
                Console.WriteLine("Please, check your SaveWork files and free up space.");
                Environment.Exit(0);
            }

            while (true)
            {
                string UserInput = Menu();
                if (_ViewModel.UserChoiceTraitement(UserInput) == 0)
                {
                    Console.Clear();
                    switch (UserInput)
                    {
                        case "1":
                            MenuChoice1();
                            break;

                        case "2":
                            Console.WriteLine($"{langage.option2}");
                            break;

                        case "3":
                            Console.WriteLine($"{langage.option3}");
                            break;

                        case "4":
                            Console.WriteLine($"{langage.option4}");
                            break;

                        case "5":
                            MenuChoice5();
                            break;
                    }
                }
            }
        }

        private string Menu()
        {
            Console.Clear();
            Console.WriteLine($"{langage.gretting}");
            Console.WriteLine();
            Console.WriteLine($"{langage.menu_choice1}");
            Console.WriteLine($"{langage.menu_choice2}");
            Console.WriteLine($"{langage.menu_choice3}");
            Console.WriteLine($"{langage.menu_choice4}");
            Console.WriteLine($"{langage.menu_choice5}");
            Console.WriteLine();
            Console.Write("==> ");

            return Console.ReadLine();
        }

        private void MenuChoice1()
        {
            string[] AttributsForSaveWork = new string[4];

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.choseName}");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[0] = Console.ReadLine();
                if (_ViewModel.TestNameSaveWork(AttributsForSaveWork[0]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.choseType}");
                Console.WriteLine();
                Console.WriteLine($"    {langage.typeComplete}");
                Console.WriteLine($"    {langage.typeDiff}");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[1] = Console.ReadLine();
                if (_ViewModel.TestTypeSaveWork(AttributsForSaveWork[1]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.sourcePath}");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[2] = Console.ReadLine();
                if (_ViewModel.TestSourcePathSaveWork(AttributsForSaveWork[2]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.destPath}");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[3] = Console.ReadLine();
                if (_ViewModel.TestDestinationPathSaveWork(AttributsForSaveWork[3]) == 0)
                    break;
            }

            Console.Clear();
            Console.WriteLine($"{langage.validation}");

            Console.WriteLine();
            PresentationOfSaveWork(AttributsForSaveWork);
            Console.WriteLine();

            Console.Write($"{langage.confirmLine}");
            if (_ViewModel.UserConfirmationTraitement(Console.ReadLine()) == 0)
            {
                _ViewModel.AddNewSaveWork(AttributsForSaveWork);
            }
        }

        private void MenuChoice5()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            string[][] SaveWorkInfos = _ViewModel.GetSaveWorkInfo();

            foreach (string[] SaveWorkInfo in SaveWorkInfos)
            {
                if (SaveWorkInfo != null)
                {
                    PresentationOfSaveWork(SaveWorkInfo);
                    Console.WriteLine("============================================================");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"{langage.exit}");
            Console.ReadKey();
        }

        private void PresentationOfSaveWork(string[] AttributsForPresentation)
        {
            Console.WriteLine($"    {langage.nameSaveWork} {AttributsForPresentation[0]}");

            Console.WriteLine($"    {langage.typeSaveWork} {AttributsForPresentation[1]}");

            Console.WriteLine($"    {langage.sourcePathSaveWork} {AttributsForPresentation[2]}");

            Console.WriteLine($"    {langage.destPathSaveWork} {AttributsForPresentation[3]}");
        }
    }
}
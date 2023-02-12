using EasySave.consoleApp.Resources;
using EasySave.consoleApp.ViewModels;
using EasySave.lib.Services;

namespace EasySave.consoleApp.Views
{
    public class View
    {
        public ViewModel _ViewModel = new ViewModel();

        public void Show()
        {
            if (_ViewModel.SaveWorkInitializing() == 1)
            {
                Console.WriteLine($"{langage.fullSaveWorks}");                                                  // The limit of SaveWork files has been exceeded.
                Console.WriteLine($"{langage.checkYourFiles}");                                                 // Please, check your SaveWork files and free up space.
                Environment.Exit(0);
            }

            while (true)
            {
                string UserInput = Menu();
                if (_ViewModel.OptionSelectedTreatment(UserInput) == 0)
                {
                    Console.Clear();
                    switch (UserInput)
                    {
                        case "1":
                            MenuChoice1();
                            break;

                        case "2":
                            MenuChoice2();
                            break;

                        case "3":
                            MenuChoice3();
                            break;

                        case "4":
                            MenuChoice4();
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
            Console.WriteLine($"{langage.gretting}");                                                           // Hi, please select a choice:
            Console.WriteLine();
            Console.WriteLine($"{langage.menu_choice1}");                                                       // [1] : Create a new SaveWork
            Console.WriteLine($"{langage.menu_choice2}");                                                       // [2] : Delete an existing SaveWork
            Console.WriteLine($"{langage.menu_choice3}");                                                       // [3] : Launch a SaveWork
            Console.WriteLine($"{langage.menu_choice4}");                                                       // [4] : Launch all the existings SaveWork
            Console.WriteLine($"{langage.menu_choice5}");                                                       // [5] : List all the existings SaveWork
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
                Console.WriteLine($"{langage.choseName}");                                                      // Please enter a name for the SaveWork :
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[0] = Console.ReadLine();
                if (_ViewModel.TestNameSaveWork(AttributsForSaveWork[0]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.choseType}");                                                      // Please enter a type for the SaveWork :
                Console.WriteLine();
                Console.WriteLine($"    {langage.typeComplete}");                                               // [1] : Complete
                Console.WriteLine($"    {langage.typeDiff}");                                                   // [2] : Differential
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[1] = Console.ReadLine();
                if (_ViewModel.TestTypeSaveWork(AttributsForSaveWork[1]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.sourcePath}");                                                     // Please enter a source path for the SaveWork :
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[2] = Console.ReadLine();
                if (_ViewModel.TestSourcePathSaveWork(AttributsForSaveWork[2]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.destPath}");                                                       // Please enter a destination path for the SaveWork :
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[3] = Console.ReadLine();
                if (_ViewModel.TestDestinationPathSaveWork(AttributsForSaveWork[3]) == 0)
                    break;
            }

            Console.Clear();
            Console.WriteLine($"{langage.validation}");                                                         // Validation step:

            Console.WriteLine();
            PresentationOfSaveWork(AttributsForSaveWork);
            Console.WriteLine();

            Console.Write($"{langage.confirmLine}");                                                            // Press [Y] for creation, [Enter] for abandon...
            if (_ViewModel.ConfirmationTreatment(Console.ReadLine()) == 0)
            {
                _ViewModel.AddNewSaveWork(AttributsForSaveWork);
            }
        }

        private void MenuChoice2()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.deletion}");
                Console.WriteLine();
                ListingOfSaveWork(_ViewModel.GetSaveWorkNames());
                Console.Write("==> ");
                string UserID = Console.ReadLine();
                if (_ViewModel.CheckSaveWorkIDTreatment(UserID) == 0)
                {
                    if (_ViewModel.RemoveSaveWork(UserID) == 1)                        // TODO (Code de retour)
                    {
                        Console.WriteLine("Remove errors occurred...");
                    }
                    Console.WriteLine("Press any key to return on the menu...");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private void MenuChoice3()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{langage.execution}");
                Console.WriteLine();
                ListingOfSaveWork(_ViewModel.GetSaveWorkNames());
                Console.Write("==> ");
                string UserID = Console.ReadLine();
                if (_ViewModel.CheckSaveWorkIDTreatment(UserID) == 0)
                {
                    if (_ViewModel.ExecuteSaveWork(UserID) == 1)
                    {
                        Console.WriteLine("Potential errors occurred...");
                    }
                    Console.WriteLine("The copy of the files is complete.");
                    Console.WriteLine("Press any key to return on the menu...");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private void MenuChoice4()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{ langage.SequentialValidation }");
                Console.WriteLine();
                ListingOfSaveWork(_ViewModel.GetSaveWorkNames());
                Console.WriteLine($"{langage.SequentialConfirm}");
                Console.WriteLine();
                Console.Write("==> ");
                if (_ViewModel.ConfirmationTreatment(Console.ReadLine()) == 0)
                {
                    if (_ViewModel.SequentialSaveWorksExecution() == 1)
                    {
                        Console.WriteLine("Potential errors occurred...");
                    }
                    Console.WriteLine("The sequencial copy of the files is complete.");
                    Console.WriteLine("Press any key to return on the menu...");
                    Console.ReadKey();
                    break;
                }
                else
                    break;
            }
        }

    private void ListingOfSaveWork(string[] NameSaveWork)
        {
            for (int i=0; i < NameSaveWork.Length; i++)
            {
                Console.Write($"    SaveWork {i + 1} : ");
                Console.WriteLine($"{NameSaveWork[i]}");
                Console.WriteLine();
            }
        }

        private void MenuChoice5()
        {
            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine();
            string[][] SaveWorkInfos = _ViewModel.GetSaveWorkInfos();

            foreach (string[] SaveWorkInfo in SaveWorkInfos)
            {
                if (SaveWorkInfo != null)
                {
                    PresentationOfSaveWork(SaveWorkInfo);
                    Console.WriteLine();
                    Console.WriteLine("============================================================");
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"{langage.exit}");                                                               // Press a key for exit...
            Console.ReadKey();
        }

        private void PresentationOfSaveWork(string[] AttributsForPresentation)
        {
            Console.WriteLine($"    {langage.nameSaveWork} {AttributsForPresentation[0]}");                     // The name of this SaveWork is :

            Console.WriteLine($"    {langage.typeSaveWork} {AttributsForPresentation[1]}");                     // The type of this SaveWork is :

            Console.WriteLine($"    {langage.sourcePathSaveWork} {AttributsForPresentation[2]}");               // The source path of this SaveWork is :

            Console.WriteLine($"    {langage.destPathSaveWork} {AttributsForPresentation[3]}");                 // The destination path of this SaveWork is :
        }
    }
}
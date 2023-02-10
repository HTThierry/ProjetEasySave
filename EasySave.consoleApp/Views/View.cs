using EasySave.consoleApp.ViewModels;
using EasySave.lib.Models;
//using EasySave.lib.Services;

namespace EasySave.consoleApp.Views
{
    public class View
    {
        public ViewModel _ViewModel = new ViewModel();
        public void Show()
        {
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
                            Console.WriteLine("Il a demandé l'option 2");
                            break;
                        case "3":
                            Console.WriteLine("Il a demandé l'option 3");
                            break;
                        case "4":
                            Console.WriteLine("Il a demandé l'option 4");
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
            Console.WriteLine("Hi, please select a choice:");
            Console.WriteLine();
            Console.WriteLine("    [1] : Create a new SaveWork");
            Console.WriteLine("    [2] : Delete an existing SaveWork");
            Console.WriteLine("    [3] : Launch a SaveWork");
            Console.WriteLine("    [4] : Launch all the existings SaveWork");
            Console.WriteLine("    [5] : List all the existings SaveWork");
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
                Console.WriteLine("Please enter a name for the SaveWork");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[0] = Console.ReadLine();
                if (_ViewModel.TestNameSaveWork(AttributsForSaveWork[0]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter a type for the SaveWork");
                Console.WriteLine();
                Console.WriteLine("    [1] : Complete");
                Console.WriteLine("    [2] : Differential");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[1] = Console.ReadLine();
                if (_ViewModel.TestTypeSaveWork(AttributsForSaveWork[1]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter a source path for the SaveWork");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[2] = Console.ReadLine();
                if (_ViewModel.TestSourcePathSaveWork(AttributsForSaveWork[2]) == 0)
                    break;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please enter a destination path for the SaveWork");
                Console.WriteLine();
                Console.Write("==> ");
                AttributsForSaveWork[3] = Console.ReadLine();
                if (_ViewModel.TestDestinationPathSaveWork(AttributsForSaveWork[3]) == 0)
                    break;
            }

            Console.Clear();
            Console.WriteLine("Validation step:");

            Console.WriteLine();
            PresentationOfSaveWork(AttributsForSaveWork);
            Console.WriteLine();

            Console.Write("Press [Y] for creation, [Enter] for abandon... ");
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
            Console.WriteLine("Press a key for exit...");
            Console.ReadKey();
        }
         
        private void PresentationOfSaveWork(string[] AttributsForPresentation)
        {
            Console.WriteLine($"    The name of this SaveWork is :               {AttributsForPresentation[0]}");

            Console.WriteLine($"    The type of this SaveWork is :               {AttributsForPresentation[1]}");

            Console.WriteLine($"    The source path of this SaveWork is :        {AttributsForPresentation[2]}");

            Console.WriteLine($"    The destination path of this SaveWork is :   {AttributsForPresentation[3]}");
        }
    }
}
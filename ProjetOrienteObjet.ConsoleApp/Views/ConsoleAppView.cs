namespace ProjetEasySave.consoleApp.Views
{
    public class ConsoleAppView
    {
        private lib.ViewModels.ConsoleAppViewModel _viewModel = new();

        public void Show()
        {
            Console.WriteLine("Type de sauvegarde :");
            Console.WriteLine("\n1 - Sauvegarde Complète (default)");
            Console.WriteLine("2 - Sauvegarde Incrementielle");
            Console.WriteLine("");
            _viewModel.Choix = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nChemin de sauvegarde initial :");
            Console.WriteLine("");
            _viewModel.PathFrom = Console.ReadLine();

            Console.WriteLine("\nChemin de sauvegarde final :");
            Console.WriteLine("");
            _viewModel.PathTo = Console.ReadLine();

            _viewModel.Sauvegarde();

            Console.WriteLine(_viewModel.Message);
        }
    }
}
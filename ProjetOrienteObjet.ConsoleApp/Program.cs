namespace ProjetOrienteObjet.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProjetEasySave.consoleApp.Views.ConsoleAppView view = new();
            ProjetEasySave.consoleApp.Views.ConsoleAppViewTemp viewTemp = new();
            viewTemp.Show();
        }
    }
}
using ProjetEasySave.lib.ViewModels;

namespace ProjetEasySave.consoleApp.Views
{
    public class ConsoleAppView
    {
        private lib.ViewModels.ConsoleAppViewModel _viewModel = new();
        private lib.ViewModels.langue langue = new();

        public void Show()
        {
            string Message = "";
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
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].FullBackup}";

                    break;

                case "2":
                    //Console.WriteLine("Sauvegarde Incrementielle");
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].IncrBackup}";

                    break;

                default:
                    //Console.WriteLine("Sauvegarde Complète");
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].FullBackup}";
                    break;
            }
            Console.WriteLine(Message);
            _viewModel.Sauvegarde();
            switch (_viewModel.Message[0])
            {
                case "backupFull":
                    Message = $"{langue.LanguageData[langue.LanguageCode].MessageFull.endCopy}{_viewModel.Message[1]} {langue.LanguageData[langue.LanguageCode].MessageFull.copyOn} {_viewModel.Message[2]}{langue.LanguageData[langue.LanguageCode].MessageFull.of}{_viewModel.Message[3]}{langue.LanguageData[langue.LanguageCode].MessageFull.to}{_viewModel.Message[4]}.";
                    break;

                case "noFiles":
                    Message = $"{langue.LanguageData[langue.LanguageCode].endCopy0}{_viewModel.Message[1]} {langue.LanguageData[langue.LanguageCode].analyse}";
                    break;

                case "copyEnd"://"Copie terminée, {_viewModel.Message[1]} fichiers modifiés sur {_viewModel.Message[2]} analysés."
                    Message = $"{langue.LanguageData[langue.LanguageCode].MessageIncr.endCopy}{_viewModel.Message[1]}{langue.LanguageData[langue.LanguageCode].MessageIncr.modifTo} {_viewModel.Message[2]} {langue.LanguageData[langue.LanguageCode].MessageIncr.analyse}";
                    break;

                default:
                    Message = $"{langue.LanguageData[langue.LanguageCode].wrongPath}";
                    break;
            }
            Console.WriteLine(Message);
        }
    }
}
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
            string Message = "";
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.title); //Type of backup :
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice1);//1 - Full Backup (default)
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].ChoiceBackup.choice2); //2 - Incremental Backup
            _viewModel.Choix = Console.ReadLine();
            //Final backup path :
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathSource);
            _viewModel.PathFrom = Console.ReadLine();
            //Initial backup path :
            Console.WriteLine(langue.LanguageData[langue.LanguageCode].PathDest);
            _viewModel.PathTo = Console.ReadLine();
            switch (_viewModel.Choix)
            {
                case "1":
                    //Console.WriteLine("Full backup");
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].FullBackup}";
                    break;

                case "2":
                    //Console.WriteLine("Incremental Backup");
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].IncrBackup}";
                    break;

                default:
                    //Console.WriteLine("Full backup");
                    Message = $"\n{langue.LanguageData[langue.LanguageCode].FullBackup}";
                    break;
            }
            Console.WriteLine(Message);
            _viewModel.Sauvegarde();
            switch (_viewModel.Message[0])
            {
                case "backupFull": //Copy completed, {_viewModel.Message[1]} files copied to {_viewModel.Message[2]} of {_viewModel.Message[3]} to {_viewModel.Message[4]}
                    Message = $"{langue.LanguageData[langue.LanguageCode].MessageFull.endCopy}{_viewModel.Message[1]} {langue.LanguageData[langue.LanguageCode].MessageFull.copyOn} {_viewModel.Message[2]}{langue.LanguageData[langue.LanguageCode].MessageFull.of}{_viewModel.Message[3]}{langue.LanguageData[langue.LanguageCode].MessageFull.to}{_viewModel.Message[4]}.";
                    break;

                case "noFiles": // Copy completed, your files are up to date, no files have been modified on {_viewModel.Message[1]} analyzed. 
                    Message = $"{langue.LanguageData[langue.LanguageCode].endCopy0}{_viewModel.Message[1]} {langue.LanguageData[langue.LanguageCode].analyse}";
                    break;

                case "copyEnd":// Copy completed, {_viewModel.Message[1]} modified files to {_viewModel.Message[2]} analyzed. 
                    Message = $"{langue.LanguageData[langue.LanguageCode].MessageIncr.endCopy}{_viewModel.Message[1]}{langue.LanguageData[langue.LanguageCode].MessageIncr.modifTo} {_viewModel.Message[2]} {langue.LanguageData[langue.LanguageCode].MessageIncr.analyse}";
                    break;
                default: // Your starting or destination path does not exist.
                    Message = $"{langue.LanguageData[langue.LanguageCode].wrongPath}";
                    break;
            }
            Console.WriteLine(Message);
        }
    }
}
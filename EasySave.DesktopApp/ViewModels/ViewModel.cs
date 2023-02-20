using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;

namespace EasySave.DesktopApp.ViewModels
{
    public class ViewModel
    {
        private Model _Model = Model.GetInstance();
        public Overwatch _Overwatch = new Overwatch();
        public Initializer _Initializer = new Initializer();
        public SaveWorkManager _SaveWorkManager = new SaveWorkManager();
        public GenerateKey _GenerateKey = new GenerateKey();
        public RunningProcess _RunningProcess = new RunningProcess();

        // A vérifier
        public int GenerateNewKey()
        {
            return _GenerateKey.Generate();
        }

        // A repenser dans une autre branche (bonus Progress State)
        public SaveWork SaveWorkCreator(string[] AttributsForSaveWork)
        {
            return _SaveWorkManager.SaveWorkCreator(AttributsForSaveWork);
        }

        // A adapter
        public int TestNameSaveWork(string SaveWorkName)
        {
            return _Overwatch.TestNameSaveWork(SaveWorkName);
        }

        // A adapter
        public int TestTypeSaveWork(string SaveWorkTypeToConvert)
        {
            return _Overwatch.TestTypeSaveWork(SaveWorkTypeToConvert);
        }

        // A adapter
        public int TestSourcePathSaveWork(string SaveWorkSourcePath)
        {
            return _Overwatch.TestSourcePathSaveWork(SaveWorkSourcePath);
        }

        // A adapter
        public int TestDestinationPathSaveWork(string SaveWorkDestinationPath)
        {
            return _Overwatch.TestDestinationPathSaveWork(SaveWorkDestinationPath);
        }

        // A vérifier
        public int AddNewSaveWork(string[] AttributsForSaveWork)
        {
            return _SaveWorkManager.AddNewSaveWork(AttributsForSaveWork, _Model.ArrayOfSaveWork);
        }

        // A vérifier
        public int SaveWorkInitializing()
        {
            return _Initializer.SaveWorkInitializing(_Model.ArrayOfSaveWork);
        }

        // A repenser
        public int RemoveSaveWorkWPF(SaveWork _SaveWork)
        {
            return _SaveWorkManager.RemoveSaveWorkWPF(_SaveWork, _Model.ArrayOfSaveWork);
        }

        // A supprimer
        public int ExecuteSaveWork(string SaveWorkID)
        {
            return _SaveWorkManager.ExecuteSaveWork(SaveWorkID, _Model.ArrayOfSaveWork);
        }

        // A repenser
        public int ExecuteSaveWorkWPF(SaveWork _SaveWork)
        {
            return _SaveWorkManager.ExecuteSaveWorkWPF(_SaveWork);
        }

        // A repenser
        public int SequentialSaveWorksExecution()
        {
            return _SaveWorkManager.SequentialSaveWorksExecution(_Model.ArrayOfSaveWork);
        }

        // A retravailler (code de retour)
        public int CheckRunningProcess(string ProcessName)
        {
            return _RunningProcess.CheckRunningProcess(ProcessName);
        }
    }
}
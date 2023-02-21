using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

namespace EasySave.DesktopApp.ViewModels
{
    public class MainViewViewModel
    {
        //private Model _Model = Model.GetInstance();
        public EntryProcessingService _EntryProcessingService = new EntryProcessingService();

        //public Initializer _Initializer = new Initializer();
        public SaveWorkManager _SaveWorkManager = new SaveWorkManager();

        public GenerateKey _GenerateKey = new GenerateKey();
        public RunningProcess _RunningProcess = new RunningProcess();

        public int GenerateNewKey()
        {
            return _GenerateKey.Generate();
        }

        //public SaveWorkService SaveWorkCreator(SaveWorkModel AttributsForSaveWork)
        //{
        //    return _SaveWorkManager.SaveWorkCreator(AttributsForSaveWork);
        //}
        public List<SaveWorkModel> GetSaveWorks()
        {
            Debug.WriteLine(_SaveWorkManager.ArrayOfSaveWork);
            return _SaveWorkManager.ArrayOfSaveWork;
        }

        public int TestNameSaveWork(string SaveWorkName)
        {
            return _SaveWorkManager.TestNameSaveWork(SaveWorkName);
        }

        public int TestTypeSaveWork(string SaveWorkTypeToConvert)
        {
            return _SaveWorkManager.TestTypeSaveWork(SaveWorkTypeToConvert);
        }

        public int TestSourcePathSaveWork(string SaveWorkSourcePath)
        {
            return _SaveWorkManager.TestSourcePathSaveWork(SaveWorkSourcePath);
        }

        public int TestDestinationPathSaveWork(string SaveWorkDestinationPath)
        {
            return _SaveWorkManager.TestDestinationPathSaveWork(SaveWorkDestinationPath);
        }

        /// <summary>
        /// Adds a SaveWork as an instance and saves it in a JSON file
        /// </summary>
        /// <param name="AttributsForSaveWork"></param>
        /// <returns></returns>
        public int AddNewSaveWork(SaveWorkModel AttributsForSaveWork)
        {
            return _SaveWorkManager.AddNewSaveWork(AttributsForSaveWork);
        }

        /// <summary>
        /// Instance all save works from the json files
        /// </summary>
        public int SaveWorkInitializing()
        {
            return _SaveWorkManager.SaveWorkInitializing();
        }

        public int RemoveSaveWorkWPF(SaveWorkModel _SaveWork)
        {
            return _SaveWorkManager.RemoveSaveWorkWPF(_SaveWork);
        }

        //public int ExecuteSaveWork(string SaveWorkID)
        //{
        //    return _SaveWorkManager.ExecuteSaveWork(SaveWorkID);
        //}

        public int ExecuteSaveWorkWPF(SaveWorkModel _SaveWork)
        {
            return _SaveWorkManager.ExecuteSaveWorkWPF(_SaveWork);
        }

        //public int SequentialSaveWorksExecution()
        //{
        //    return _SaveWorkManager.SequentialSaveWorksExecution(_Model.ArrayOfSaveWork);
        //}

        public bool CheckRunningProcess(string ProcessName)
        {
            return _RunningProcess.CheckRunningProcess(ProcessName);
        }

        public void LaunchAllCommand()
        {
            foreach (SaveWorkModel _saveWork in _SaveWorkManager.ArrayOfSaveWork)
            {
                while (CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
                {
                    CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
                    _SoftwarePackage.ShowDialog();
                }
                ExecuteSaveWorkWPF(_saveWork);
            }
        }

        public void LaunchCommand(SaveWorkModel model)
        {
            while (CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
            {
                CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
                _SoftwarePackage.ShowDialog();
            }

            if (model != null)
            {
                int x = ExecuteSaveWorkWPF(model);
                if (x == 2) { }
            }
        }
    }
}
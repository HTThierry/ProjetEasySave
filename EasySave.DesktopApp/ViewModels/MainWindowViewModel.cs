using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace EasySave.DesktopApp.ViewModels
{
    public class MainWindowViewModel
    {
        //private Model _Model = Model.GetInstance();
        public EntryProcessingService _EntryProcessingService = new EntryProcessingService();

        //public Initializer _Initializer = new Initializer();
        public SaveWorkManager _SaveWorkManager = new SaveWorkManager();

        public GenerateKey _GenerateKey = new GenerateKey();

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

        

        /// <summary>
        /// Adds a SaveWork as an instance and saves it in a JSON file
        /// </summary>
        /// <param name="AttributsForSaveWork"></param>
        /// <returns></returns>
        public void AddNewSaveWork(SaveWorkModel AttributsForSaveWork)
        {
            _SaveWorkManager.AddNewSaveWork(AttributsForSaveWork);
        }

        /// <summary>
        /// Instance all save works from the json files
        /// </summary>
        public void SaveWorkInitializing()
        {
            
            _SaveWorkManager.SaveWorkInitializing();
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
            return _SaveWorkManager.CheckRunningProcess(ProcessName);
        }

        public void LaunchAllCommand()
        {
            foreach (SaveWorkModel _saveWork in _SaveWorkManager.ArrayOfSaveWork)
            {
                while (CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
                {
                    Debug.WriteLine("Un logiciel métier est actif");
                    MessageBox.Show("Un logiciel métier est actif");
                }
                ExecuteSaveWorkWPF(_saveWork);
            }
        }

        public void LaunchCommand(SaveWorkModel model)
        {
            while (CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
            {
                Debug.WriteLine("Un logiciel métier est actif");
                MessageBox.Show("Un logiciel métier est actif");
            }

            if (model != null)
            {
                int x = ExecuteSaveWorkWPF(model);
                if (x == 2) { }
            }
        }
    }
}
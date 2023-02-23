using EasySave.lib.Models;
using EasySave.lib.Services;
using EasySave.lib.Services.Server;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace EasySave.DesktopApp.ViewModels
{
    public class MainWindowViewModel
    {
        //private Model _Model = Model.GetInstance();

        //public Initializer _Initializer = new Initializer();
        public SaveWorkManager _SaveWorkManager = new SaveWorkManager();

        public GenerateKey _GenerateKey = new GenerateKey();
        public SaveWorkService SaveWorkService = new SaveWorkService();
        public Server Server = new Server();
        public SaveWorkModel _SaveWorkModel;

        private static void ProgessState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine(e.PropertyName);
        }

        public void OpenSocket()
        {
            Server.Start();
        }

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
        /// Adds a SaveWork as an instance and saves it in a JSON file
        /// </summary>
        /// <param name="AttributsForSaveWork"></param>
        /// <returns></returns>
        public int UpdateSaveWork(SaveWorkModel AttributsForSaveWork, SaveWorkModel OldSaveWork)
        {
            return _SaveWorkManager.UpdateSaveWork(AttributsForSaveWork, OldSaveWork);
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

        public void ExecuteSaveWorkWPF(SaveWorkModel _SaveWork)
        {
            _SaveWorkModel = _SaveWork;
            _SaveWorkModel.PropertyChanged += ProgessState_PropertyChanged;
            _SaveWorkManager.ExecuteSaveWorkWPF(_SaveWorkModel);
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

            ExecuteSaveWorkWPF(model);
        }

        public void pauseSaveWork(SaveWorkModel model)
        {
            SaveWorkService.PauseSaveWork(model);
        }
        public void resumeSaveWork(SaveWorkModel model)
        {
            SaveWorkService.ResumeSaveWork(model);
        }
    }
}
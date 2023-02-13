using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;

namespace EasySave.DesktopApp.ViewModels
{
    public class ViewModel
    {
        public Model _Model { get; set; } = new Model(); 
        public InputProcessingService _InputProcessingService = new InputProcessingService();
        public EntryProcessingService _EntryProcessingService = new EntryProcessingService();
        public Initializer _Initializer = new Initializer();
        public Presenter _Presenter = new Presenter();
        public SaveWorkManager _SaveWorkManager = new SaveWorkManager();

        public SaveWork SaveWorkCreator(string[] AttributsForSaveWork)
        {
            return _SaveWorkManager.SaveWorkCreator(AttributsForSaveWork);
        }

        public int TestNameSaveWork(string SaveWorkName)
        {
            return _EntryProcessingService.TestNameSaveWork(SaveWorkName);
        }

        public int TestTypeSaveWork(string SaveWorkTypeToConvert)
        {
            return _EntryProcessingService.TestTypeSaveWork(SaveWorkTypeToConvert);
        }

        public int TestSourcePathSaveWork(string SaveWorkSourcePath)
        {
            return _EntryProcessingService.TestSourcePathSaveWork(SaveWorkSourcePath);
        }

        public int TestDestinationPathSaveWork(string SaveWorkDestinationPath)
        {
            return _EntryProcessingService.TestDestinationPathSaveWork(SaveWorkDestinationPath);
        }

        /// <summary>
        /// Adds a SaveWork as an instance and saves it in a JSON file
        /// </summary>
        /// <param name="AttributsForSaveWork"></param>
        /// <returns></returns>
        public int AddNewSaveWork(string[] AttributsForSaveWork)
        {
            return _SaveWorkManager.AddNewSaveWork(AttributsForSaveWork, _Model.ArrayOfSaveWork);
        }
        
        /// <summary>
        /// Instance all save works from the json files
        /// </summary>
        public int SaveWorkInitializing()
        {
            return _Initializer.SaveWorkInitializing(_Model.ArrayOfSaveWork);
        }

        public string[][] GetSaveWorkInfos()
        {
            return _Presenter.GetSaveWorkInfos(_Model.ArrayOfSaveWork);
        }

        public string[] GetSaveWorkNames()
        {
            return _Presenter.GetSaveWorkNames(_Model.ArrayOfSaveWork);
        }

        public int RemoveSaveWorkWPF(SaveWork _SaveWork)
        {

            return _SaveWorkManager.RemoveSaveWorkWPF(_SaveWork, _Model.ArrayOfSaveWork);
        }

        public int ExecuteSaveWork(string SaveWorkID)
        {
            return _SaveWorkManager.ExecuteSaveWork(SaveWorkID, _Model.ArrayOfSaveWork);
        }

        public int SequentialSaveWorksExecution()
        {
            return _SaveWorkManager.SequentialSaveWorksExecution(_Model.ArrayOfSaveWork);
        }

        public int ReturnModelList(List<SaveWork> _ArrayOfSaveWork)
        {
            _Model.ArrayOfSaveWork= _ArrayOfSaveWork;
            return 0;
        }
    }
}
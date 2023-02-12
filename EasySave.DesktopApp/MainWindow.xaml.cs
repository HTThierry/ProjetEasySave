using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;
using System.Windows;
using EasySave.DesktopApp.ViewModels;
using System.Collections.ObjectModel;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _ViewModel = new ViewModel();
        private Model _Model = new Model();
        private Presenter _Presenter = new Presenter();
        private Initializer _Initializer = new Initializer();
        private SaveWorkModel _SaveWorkModel = new SaveWorkModel();


        public string[] GetInstanceInfo()
        {
            string[] AttributsForPresentation = new string[4] { _SaveWorkModel.NameSaveWork, $"{_SaveWorkModel.TypeSaveWork}", _SaveWorkModel.SourcePathSaveWork, _SaveWorkModel.DestinationPathSaveWork };
            return AttributsForPresentation;
        }

        public int SaveWorkInitializing()
        {
            return _Initializer.SaveWorkInitializing(_Model.ArrayOfSaveWork);
        }

        public string[][] GetSaveWorkInfos()
        {
            return _Presenter.GetSaveWorkInfos(_Model.ArrayOfSaveWork);
        }

        public MainWindow()
        {
            InitializeComponent();
            SaveWorkInitializing();

            dgSaveWorks.ItemsSource = _Model.ArrayOfSaveWork;
        }

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {
        }

        public void LaunchAllCommand(object sender, RoutedEventArgs e)
        {
        }

        public void LaunchCommand(object sender, RoutedEventArgs e)
        {
            SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;
            if (selectedSaveWork != null)
            {
                selectedSaveWork.LaunchSaveWork();
            }
        }

        public void deleteCommand(object sender, RoutedEventArgs e)
        {
            SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;
            _ViewModel.RemoveSaveWorkWPF(selectedSaveWork);
            //dgSaveWorks.
        }
    }
}
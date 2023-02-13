using EasySave.lib.Services;
using EasySave.lib.Models;
using EasySave.DesktopApp.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System;
using System.Windows.Navigation;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _Model = new Model();
        private Presenter _Presenter = new Presenter();
        private Initializer _Initializer = new Initializer();
        private SaveWorkModel _SaveWorkModel = new SaveWorkModel();
        private ViewModel _ViewModel = new ViewModel();

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
            NavigationService navService = NavigationService.GetNavigationService(this);
            dgSaveWorks.ItemsSource = _Model.ArrayOfSaveWork;
            _ViewModel.ReturnModelList(_Model.ArrayOfSaveWork);
        }

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {
            
            AddSaveWork NewsPage= new AddSaveWork();

            NewsPage.ShowDialog();

        }
        public void RefreshSaveWorks()
        {
            // Mettre à jour la liste de SaveWork dans le DataGrid
            dgSaveWorks.Items.Refresh();
        }
        public void LaunchAllCommand(object sender, RoutedEventArgs e)
        {
            foreach (SaveWork _saveWork in _Model.ArrayOfSaveWork)
            {
                _saveWork.LaunchSaveWork();
            }
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
            dgSaveWorks.Items.Refresh();

        }
    }
}
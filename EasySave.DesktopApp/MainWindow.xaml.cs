using EasySave.DesktopApp.ViewModels;
using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Windows;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Model _Model { get; set; } = new Model();
        private ViewModel _ViewModel = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            _ViewModel.SaveWorkInitializing();

            SaveWorkInitializing();
            _ViewModel.generateNewKey();
            //set ArrayOfSaveWork in datagrid
            dgSaveWorks.ItemsSource = _Model.ArrayOfSaveWork;

            //force link _Model.ArrayOfSaveWork
            _ViewModel.ReturnModelList(_Model.ArrayOfSaveWork);
        }

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {
            AddSaveWork NewsPage = new AddSaveWork();
            // Wait for the user to close the windows
            NewsPage.ShowDialog();
            //get the data the user set in the windows pop-up
            string[] savedVariable = NewsPage.AttributsForSaveWork;
            if (savedVariable != null)
            {
                _ViewModel.AddNewSaveWork(savedVariable);
                dgSaveWorks.Items.Refresh();
            }
        }

        public void RefreshSaveWorks()
        {
            // refresh the datagrid
            dgSaveWorks.Items.Refresh();
        }

        public void LaunchAllCommand(object sender, RoutedEventArgs e)
        {
            foreach (SaveWork _saveWork in _Model.ArrayOfSaveWork)
            {
                while (_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
                {
                    CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
                    _SoftwarePackage.ShowDialog();
                }
                _ViewModel.ExecuteSaveWorkWPF(_saveWork);
            }
        }

        public async void LaunchCommand(object sender, RoutedEventArgs e)
        {
            SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;

            while(_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
            {
                CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
                _SoftwarePackage.ShowDialog();
            }

            if (selectedSaveWork != null)
            {
                int x = _ViewModel.ExecuteSaveWorkWPF(selectedSaveWork);
                if (x == 2) { }
            }
        }

        public void deleteCommand(object sender, RoutedEventArgs e)
        {
            SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;

            _ViewModel.RemoveSaveWorkWPF(selectedSaveWork);
            RefreshSaveWorks();
        }
        public void generateCommand(object sender, RoutedEventArgs e)
        {
            _ViewModel.generateNewKey();
        }
    }
}
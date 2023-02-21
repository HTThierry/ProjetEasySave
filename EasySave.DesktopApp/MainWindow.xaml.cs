using EasySave.DesktopApp.ViewModels;
using EasySave.lib.Models;
using EasySave.lib.Services;
using System.Configuration;
using System.Windows;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Model _Model = Model.GetInstance();
        private MainWindowViewModel ViewModel = new MainWindowViewModel();
        public 

        public MainWindow()
        {
            InitializeComponent();

            //_ViewModel.GenerateNewKey();
            //_ViewModel.SaveWorkInitializing();

            ////set ArrayOfSaveWork in datagrid
            //dgSaveWorks.ItemsSource = _Model.ArrayOfSaveWork;
        }

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {
            SaveWorkControlPanel NewsPage = new SaveWorkControlPanel();
            // Wait for the user to close the windows
            NewsPage.ShowDialog();
            //get the data the user set in the windows pop-up
            Model savedVariable = NewsPage.AttributsForSaveWork;
            if (savedVariable != null)
            {
                //ViewModel.AddNewSaveWork(savedVariable);
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
            //foreach (SaveWork _saveWork in _Model.ArrayOfSaveWork)
            //{
            //    while (_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == 0)
            //    {
            //        CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
            //        _SoftwarePackage.ShowDialog();
            //    }
            //    _ViewModel.ExecuteSaveWorkWPF(_saveWork);
            //}
        }

        public async void LaunchCommand(object sender, RoutedEventArgs e)
        {
            //SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;

            //while (_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == 0)
            //{
            //    CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
            //    _SoftwarePackage.ShowDialog();
            //}

            //if (selectedSaveWork != null)
            //{
            //    int x = _ViewModel.ExecuteSaveWorkWPF(selectedSaveWork);
            //    if (x == 2) { }
            //}
        }

        public void deleteCommand(object sender, RoutedEventArgs e)
        {
            //SaveWork selectedSaveWork = dgSaveWorks.SelectedItem as SaveWork;

            //_ViewModel.RemoveSaveWorkWPF(selectedSaveWork);
            //RefreshSaveWorks();
        }

        public void generateCommand(object sender, RoutedEventArgs e)
        {
            //_ViewModel.GenerateNewKey();
        }

        public void Supprimer_Click(object sender, RoutedEventArgs e) { }
        public void Pause_Click(object sender, RoutedEventArgs e) { }
        public void Lancer_Click(object sender, RoutedEventArgs e) { }
        public void Stop_Click(object sender, RoutedEventArgs e) { }
        public void Visual_Click(object sender, RoutedEventArgs e) { }

        private void dgSaveWorks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
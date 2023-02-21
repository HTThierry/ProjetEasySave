using EasySave.DesktopApp.ViewModels;
using EasySave.lib.Models;
using System.Windows;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Model _Model = Model.GetInstance();
        private MainViewViewModel _ViewModel = new MainViewViewModel();

        public MainWindow()
        {
            InitializeComponent();

            _ViewModel.GenerateNewKey();
            _ViewModel.SaveWorkInitializing();

            //set ArrayOfSaveWork in datagrid
            dgSaveWorks.ItemsSource = _ViewModel.GetSaveWorks();
        }

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {
            AddSaveWork NewsPage = new AddSaveWork();
            // Wait for the user to close the windows
            NewsPage.ShowDialog();
            //get the data the user set in the windows pop-up
            SaveWorkModel savedVariable = NewsPage.SaveWork;
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
            //foreach (SaveWorkService _saveWork in _Model.ArrayOfSaveWork)
            //{
            //    while (_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
            //    {
            //        CloseSoftwarePackage _SoftwarePackage = new CloseSoftwarePackage();
            //        _SoftwarePackage.ShowDialog();
            //    }
            //    _ViewModel.ExecuteSaveWorkWPF(_saveWork);
            //}
            _ViewModel.LaunchAllCommand();
        }

        public async void LaunchCommand(object sender, RoutedEventArgs e)
        {
            SaveWorkModel selectedSaveWork = dgSaveWorks.SelectedItem as SaveWorkModel;
            _ViewModel.LaunchCommand(selectedSaveWork);
            //while (_ViewModel.CheckRunningProcess(ConfigurationManager.AppSettings["RunningProcess"]) == true)
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
            SaveWorkModel selectedSaveWork = dgSaveWorks.SelectedItem as SaveWorkModel;

            _ViewModel.RemoveSaveWorkWPF(selectedSaveWork);
            RefreshSaveWorks();
        }

        public void generateCommand(object sender, RoutedEventArgs e)
        {
            _ViewModel.GenerateNewKey();
        }

        public void Supprimer_Click(object sender, RoutedEventArgs e)
        { }

        public void Pause_Click(object sender, RoutedEventArgs e)
        { }

        public void Lancer_Click(object sender, RoutedEventArgs e)
        { }

        public void Stop_Click(object sender, RoutedEventArgs e)
        { }

        public void Visual_Click(object sender, RoutedEventArgs e)
        { }
    }
}
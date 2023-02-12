using EasySave.lib.Models;
using EasySave.lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model _Model = new Model();
        Presenter _Presenter = new Presenter();
        Initializer _Initializer = new Initializer();
        SaveWorkModel _SaveWorkModel = new SaveWorkModel();
        SaveWork _saveWork = new SaveWork();

        public MainWindow()
        {
            InitializeComponent();
            SaveWorkInitializing();
            List<SaveWork> test = _Model.ArrayOfSaveWork;
            dgSaveWorks.ItemsSource= test.ToList();
        }

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

        public void AddSaveWorkCommand(object sender, RoutedEventArgs e)
        {

        }

        public void LaunchAllCommand(object sender, RoutedEventArgs e)
        {

        }
        public void LaunchCommand(object sender, RoutedEventArgs e)
        {


        }
        public static void deleteCommand(string name)
        {



        }

        private void dgSaveWorks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

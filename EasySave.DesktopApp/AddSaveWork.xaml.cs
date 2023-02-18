using EasySave.DesktopApp.ViewModels;
using EasySave.DesktopApp.Resources;
using System.Windows;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Logique d'interaction pour AddSaveWork.xaml
    /// </summary>
    public partial class AddSaveWork : Window
    {
        private ViewModel _ViewModel = new ViewModel();
        public string[] AttributsForSaveWork { get; private set; }

        public AddSaveWork()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //for the windows
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // get the data of the new saveWork
            string name = NameTextBox.Text;
            string type = (TypeComboBox.SelectedIndex == 0) ? "1" : "2";
            string sourcePath = SourcePathTextBox.Text;
            string destinationPath = DestinationPathTextBox.Text;

            int verifName = _ViewModel.TestNameSaveWork(name);
            int verifPathSource = _ViewModel.TestSourcePathSaveWork(sourcePath);
            int verifPathDest= _ViewModel.TestDestinationPathSaveWork(destinationPath);

            if (verifName==1 || verifPathSource==1 || verifPathDest==1)
            {
                if (verifName==1)
                {
                    MessageBox.Show($"{langage.ErrorName}");
                }
                if (verifPathSource == 1)
                {
                    MessageBox.Show($"{langage.ErrorPathSource}");
                }
                if (verifPathDest == 1)
                {
                    MessageBox.Show($"{langage.ErrorPathDest}");
                }
            }
            else
            {
                this.AttributsForSaveWork = new string[4] { name, type, sourcePath, destinationPath };

                Close();
            }

            
        }

        private void SourcePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            //A faire

        }

        private void DestinationPathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            //A faire
        }
    }
}
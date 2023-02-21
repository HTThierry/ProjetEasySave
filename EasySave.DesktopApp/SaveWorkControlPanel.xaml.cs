using EasySave.DesktopApp.ViewModels;
using EasySave.lib.Models;
using EasySave.DesktopApp.Resources;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Logique d'interaction pour SaveWorkControlPanel.xaml
    /// </summary>
    public partial class SaveWorkControlPanel : Window
    {
        //private ViewModel _ViewModel = new ViewModel();
        public SaveWorkModel AttributsForSaveWork { get; set; }

        public SaveWorkControlPanel()
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

            AttributsForSaveWork.NameSaveWork = NameTextBox.Text;
            AttributsForSaveWork.TypeSaveWork = (TypeComboBox.SelectedIndex == 0) ? 1 : 2;
            AttributsForSaveWork.SourcePathSaveWork = SourcePathTextBox.Text;
            AttributsForSaveWork.DestinationPathSaveWork = DestinationPathTextBox.Text;


            Close();

            //int verifName = _ViewModel.TestNameSaveWork(name);
            //int verifPathSource = _ViewModel.TestSourcePathSaveWork(sourcePath);
            //int verifPathDest= _ViewModel.TestDestinationPathSaveWork(destinationPath);

            //if (verifName==1 || verifPathSource==1 || verifPathDest==1)
            //{
            //    if (verifName==1)
            //    {
            //        MessageBox.Show($"{langage.ErrorName}");
            //    }
            //    if (verifPathSource == 1)
            //    {
            //        MessageBox.Show($"{langage.ErrorPathSource}");
            //    }
            //    if (verifPathDest == 1)
            //    {
            //        MessageBox.Show($"{langage.ErrorPathDest}");
            //    }
            //}
            //else
            //{
            //    this.AttributsForSaveWork = new string[4] { name, type, sourcePath, destinationPath };

            //    Close();
            //}


        }

        private void SourcePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                string selectedDirectory = dialog.SelectedPath;
                SourcePathTextBox.Text = selectedDirectory;
            }

        }

        private void DestinationPathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedDirectory = dialog.SelectedPath;
                DestinationPathTextBox.Text = selectedDirectory;
            }
        }
    }
}
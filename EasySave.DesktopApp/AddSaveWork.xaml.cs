using EasySave.DesktopApp.Resources;
using EasySave.DesktopApp.ViewModels;
using EasySave.lib.Models;
using System;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Logique d'interaction pour AddSaveWork.xaml
    /// </summary>
    public partial class AddSaveWork : Window
    {
        private MainViewViewModel _ViewModel = new MainViewViewModel();

        //public string[] AttributsForSaveWork { get; private set; }
        public SaveWorkModel SaveWork = new SaveWorkModel();

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
            int verifPathDest = _ViewModel.TestDestinationPathSaveWork(destinationPath);

            if (verifName == 1 || verifPathSource == 1 || verifPathDest == 1)
            {
                if (verifName == 1)
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
                //this.AttributsForSaveWork = new string[4] { name, type, sourcePath, destinationPath };

                SaveWork.NameSaveWork = name;
                SaveWork.TypeSaveWork = Int32.Parse(type);
                SaveWork.SourcePathSaveWork = sourcePath;
                SaveWork.DestinationPathSaveWork = destinationPath;

                Close();
            }
        }

        private void SourcePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
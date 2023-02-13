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
using System.Windows.Shapes;
using EasySave.lib.Services;
using EasySave.lib.Models;
using EasySave.DesktopApp.ViewModels;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Logique d'interaction pour AddSaveWork.xaml
    /// </summary>
    public partial class AddSaveWork : Window
    {
        private ViewModel _ViewModel = new ViewModel();

        public AddSaveWork()
        {
            InitializeComponent();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la fenêtre pop-up sans sauvegarder le SaveWork
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Sauvegarder le nouveau SaveWork
            string name = NameTextBox.Text;
            string type = (TypeComboBox.SelectedIndex == 0) ? "1" : "2";
            string sourcePath = SourcePathTextBox.Text;
            string destinationPath = DestinationPathTextBox.Text;

            string[] AttributsForSaveWork = new string[4] { name, type, sourcePath, destinationPath };


            
            Close();
        }
    }
}

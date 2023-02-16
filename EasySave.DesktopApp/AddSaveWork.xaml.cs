using System.Windows;

namespace EasySave.DesktopApp
{
    /// <summary>
    /// Logique d'interaction pour AddSaveWork.xaml
    /// </summary>
    public partial class AddSaveWork : Window
    {
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

            this.AttributsForSaveWork = new string[4] { name, type, sourcePath, destinationPath };

            Close();
        }

        private void SourcePathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DestinationPathBrowseButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EasySave.wpfApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }
        public void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void btnKill_Click(object sender, RoutedEventArgs e)
        {
            Process selectedProcess = dgProcess.SelectedItem as Process;
            if (selectedProcess != null)
            {
                selectedProcess.Kill();
            }
        }
        public void btnAddProcess_Click(object sender, RoutedEventArgs e)
        {


        }
        public static void startProcess(string name)
        {



        }
    }
}

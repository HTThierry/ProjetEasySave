using EasySave.DistentClient.Services;
using EasySave.DistentClient.Models;
using static EasySave.DistentClient.Services.Listener;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace EasySave.DistentClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<SaveWorkModel> SaveWork { get; } = new ObservableCollection<SaveWorkModel>();
        Listener _Listener = new(55263);
        
        public void ConnectSocket()
        {
           _Listener.Start();
           _Listener.msgRecevie = Listener_msgRecevie;
        }

        public void ExecuteSaveWorkWPF()
        {
        }

        public void LaunchAllCommand()
        {
        }

        public void LaunchCommand()
        {
        }

        public void pauseSaveWork()
        {
        }

        public void resumeSaveWork()
        {
        }

        private void Listener_msgRecevie(object sender, msgEventArgs e)
        {
            SaveWorkModel saveWorkJSON = JsonSerializer.Deserialize<SaveWorkModel>(e.msg)!;
            SaveWork.Add(saveWorkJSON);
        }
    }
}
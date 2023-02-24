using EasySave.DistentClient.Services;
using EasySave.DistentClient.Models;
using static EasySave.DistentClient.Services.Listener;
using System.Text.Json;
using System.Collections.ObjectModel;
using System;

namespace EasySave.DistentClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ModelForSocket> SaveWork { get; } = new ObservableCollection<ModelForSocket>();
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
            ModelForSocket saveWorkJSON = new ModelForSocket()
            {
                NameSaveWork = e.msg[0],
                TypeSaveWork = Int32.Parse(e.msg[1]),
                SourcePathSaveWork = e.msg[2],
                DestinationPathSaveWork = e.msg[3],
                ProgressState = Int32.Parse(e.msg[4])
            };
            SaveWork.Add(saveWorkJSON);
        }
    }
}
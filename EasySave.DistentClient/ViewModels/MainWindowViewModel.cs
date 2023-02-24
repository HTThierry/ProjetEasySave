using EasySave.DistentClient.Models;
using EasySave.DistentClient.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using static EasySave.DistentClient.Services.Listener;

namespace EasySave.DistentClient.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ModelForSocket> SaveWork { get; } = new ObservableCollection<ModelForSocket>();
        private Listener _Listener = new(55263);

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
            int index = 0;
            bool notNewModel = false;

            ModelForSocket saveWorkJSON = new ModelForSocket()
            {
                NameSaveWork = e.msg[0],
                TypeSaveWork = Int32.Parse(e.msg[1]),
                SourcePathSaveWork = e.msg[2],
                DestinationPathSaveWork = e.msg[3],
                ProgressState = e.msg[4],
                Percentage = float.Parse(e.msg[5])
            };

            for (int i = 0; i < SaveWork.Count; i++)
            {
                if (SaveWork[i].NameSaveWork == saveWorkJSON.NameSaveWork)
                {
                    index = i;
                    notNewModel = true;
                }
                else
                {
                    notNewModel = false;
                }
            }
            if (notNewModel)

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    SaveWork[index] = saveWorkJSON;
                }));
            else
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    SaveWork.Add(saveWorkJSON);
                }));
            }
        }
    }
}
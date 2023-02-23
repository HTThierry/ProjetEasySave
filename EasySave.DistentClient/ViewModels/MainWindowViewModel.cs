using EasySave.DistentClient.Services;

namespace EasySave.DistentClient.ViewModels
{
    public class MainWindowViewModel
    {
        Listener Listener = new(55263);
        public void ConnectSocket()
        {
            Listener.Start();
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
    }
}
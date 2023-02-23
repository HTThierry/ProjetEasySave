using System.ComponentModel;
using System.Threading;

namespace EasySave.DistentClient.Models
{
    public class SaveWorkModel : INotifyPropertyChanged
    {
        public string NameSaveWork { get; set; }

        public int TypeSaveWork { get; set; }

        public string SourcePathSaveWork { get; set; }

        public string DestinationPathSaveWork { get; set; }

        public ProgressStateModel progressStateModel { get; set; } = new ProgressStateModel();
        public ProgressStateModel ProgressStateModel
        {
            get { return progressStateModel; }
            set
            {
                progressStateModel = value;
                OnPropertyChanged("ProgressStateModel");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
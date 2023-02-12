using System.ComponentModel;

namespace EasySave.lib.Models
{
    public class SaveWorkModel : INotifyPropertyChanged
    {
        private string _NameSaveWork;
        public string NameSaveWork
        {
            get { return _NameSaveWork; }
            set
            {
                _NameSaveWork = value;
                OnPropertyChanged("NameSaveWork");
            }
        }

        private int _TypeSaveWork;
        public int TypeSaveWork
        {
            get { return _TypeSaveWork; }
            set
            {
                _TypeSaveWork = value;
                OnPropertyChanged("TypeSaveWork");
            }
        }

        private string _SourcePathSaveWork;
        public string SourcePathSaveWork
        {
            get { return _SourcePathSaveWork; }
            set
            {
                _SourcePathSaveWork = value;
                OnPropertyChanged("SourcePathSaveWork");
            }
        }

        private string _DestinationPathSaveWork;
        public string DestinationPathSaveWork
        {
            get { return _DestinationPathSaveWork; }
            set
            {
                _DestinationPathSaveWork = value;
                OnPropertyChanged("DestinationPathSaveWork");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
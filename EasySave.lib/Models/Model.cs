using EasySave.lib.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasySave.lib.Models
{
    public class Model //: INotifyPropertyChanged
    {
        public List<SaveWork> ArrayOfSaveWork = new List<SaveWork>();

        //public List<SaveWork> ArrayOfSaveWork
        //{
        //    get { return _arrayOfSaveWork; }
        //    set
        //    {
        //        if (_arrayOfSaveWork != value)
        //        {
        //            _arrayOfSaveWork = value;
        //            RaisePropertyChanged("ArrayOfSaveWork");
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;
        //public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
    
    
}
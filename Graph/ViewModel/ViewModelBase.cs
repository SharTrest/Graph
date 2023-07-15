using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Graph.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string Name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
    }
    }

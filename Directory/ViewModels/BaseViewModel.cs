using PropertyChanged;
using System.ComponentModel;
namespace FileExplorer
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion

    }
}
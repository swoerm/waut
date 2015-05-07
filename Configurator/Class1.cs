using System.ComponentModel;
using System.Runtime.Serialization;

namespace Waut.Configurator
{
    public class CommonBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged (string PropertyName)
        {

        }

        #endregion INotifyPropertyChanged
    }
}

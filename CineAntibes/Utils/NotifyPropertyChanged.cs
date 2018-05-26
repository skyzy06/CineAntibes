using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CineAntibes.Utils
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// Event used to implement the INotifyPropertyChanged interface.
        /// This is used to notify the view (meaning the page of Property changes in this ModelView).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

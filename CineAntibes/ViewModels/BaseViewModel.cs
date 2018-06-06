using CineAntibes.Utils;
using Xamarin.Forms;

namespace CineAntibes.ViewModels
{
    public class BaseViewModel : NotifyPropertyChanged
    {
        public INavigation GetCurrentNavigation()
        {
            return (Application.Current.MainPage as MasterDetailPage).Detail.Navigation;
        }
    }
}

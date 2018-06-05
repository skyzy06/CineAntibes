using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CineAntibes.Views
{
    public partial class Loading : ContentPage
    {
        public Loading()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public LoadingViewModel GetContext()
        {
            return BindingContext as LoadingViewModel;
        }

        public ProgressBar GetProgressBar(){
            return progressBar;
        }
    }
}

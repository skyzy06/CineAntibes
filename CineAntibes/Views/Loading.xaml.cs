using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

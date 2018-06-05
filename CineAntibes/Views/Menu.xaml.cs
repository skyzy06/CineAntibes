using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    /// <summary>
    /// Menu page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Icon = "hamburger.png";
            }

            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public MenuViewModel GetContext(){
            return BindingContext as MenuViewModel;
        }
    }
}

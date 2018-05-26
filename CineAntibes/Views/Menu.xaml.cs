using CineAntibes.ViewModels;
using Xamarin.Forms;
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
        }

        public MenuViewModel GetContext(){
            return BindingContext as MenuViewModel;
        }
    }
}

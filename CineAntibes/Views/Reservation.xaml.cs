using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reservation : ContentPage
    {
        public Reservation()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public ReservationViewModel GetContext()
        {
            return BindingContext as ReservationViewModel;
        }
    }
}

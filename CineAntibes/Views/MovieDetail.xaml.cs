using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetail : ContentPage
    {
        public MovieDetail()
        {
            InitializeComponent();
        }

        public MovieDetailViewModel GetContext()
        {
            return BindingContext as MovieDetailViewModel;
        }
    }
}

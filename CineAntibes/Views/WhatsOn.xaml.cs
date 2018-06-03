using CineAntibes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhatsOn : ContentPage
    {
        public WhatsOn()
        {
            InitializeComponent();
        }

        public WhatsOnViewModel GetContext()
        {
            return BindingContext as WhatsOnViewModel;
        }
    }
}

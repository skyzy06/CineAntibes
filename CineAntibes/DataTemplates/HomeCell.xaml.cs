using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CineAntibes.DataTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeCell : ContentView
    {
        public HomeCell()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(HomeCell), "", BindingMode.TwoWay, propertyChanged: OnTitlePropertyChanged);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        static void OnTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((HomeCell)bindable).title.Text = newValue.ToString();
        }


        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create("Image", typeof(string), typeof(HomeCell), "", BindingMode.TwoWay, propertyChanged: OnImagePropertyChanged);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        static void OnImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HomeCell)bindable;
            ((HomeCell)bindable).icon.Source = newValue.ToString();
        }
    }
}

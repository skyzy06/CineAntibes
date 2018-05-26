using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CineAntibes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : MasterDetailPage
    {
        public Main()
        {
            InitializeComponent();
            (Master as Menu).GetContext().MainPage = this;

        }
    }
}

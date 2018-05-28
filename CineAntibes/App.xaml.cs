using System;
using CineAntibes.Database;
using CineAntibes.Utils;
using CineAntibes.Views;
using Xamarin.Forms;

namespace CineAntibes
{
    public partial class App : Application
    {
        public EventHandler OnUseCustomTheme;

        public App()
        {
            InitializeComponent();

            MainPage = new Main();

            WebService.GetCinemaInformations();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            // apply the color theme
            Device.BeginInvokeOnMainThread(() =>
            {
                UseCustomTheme();
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #region Theme
        public void UseCustomTheme()
        {
            OnUseCustomTheme?.Invoke(Current, null);
        }
        #endregion

        #region WebService
        WebServiceReference webService;
        public WebServiceReference WebService
        {
            get
            {
                if (webService == null) webService = new WebServiceReference();
                return webService;
            }
        }
        #endregion

        #region Database
        static CinemaTable cinemaTable;
        public static CinemaTable CinemaTable
        {
            get
            {
                if (cinemaTable == null) cinemaTable = new CinemaTable();
                return cinemaTable;
            }
        }

        static PriceTable priceTable;
        public static PriceTable PriceTable
        {
            get
            {
                if (priceTable == null) priceTable = new PriceTable();
                return priceTable;
            }
        }
        #endregion
    }
}

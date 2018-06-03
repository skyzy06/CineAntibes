using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CineAntibes.Database;
using CineAntibes.Utils;
using CineAntibes.Views;
using Xamarin.Forms;

namespace CineAntibes
{
    public partial class App : Application
    {
        public EventHandler OnUseCustomTheme;
        public static CultureInfo CinemaCulture = new CultureInfo("fr-FR");
        public static string DecimalSep =
            DependencyService.Get<ILocalize>().GetCurrentCultureInfo()
                             .NumberFormat.NumberDecimalSeparator;

        public App()
        {
            InitializeComponent();

            MainPage = new Main();

            Task cineInformations = new Task(
                async () => await WebService.GetCinemaInformations()
            );
            cineInformations.Start();
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

        #region Menu Item Filter
        static List<string> whatsOnList;
        public static List<string> WhatsOnList
        {
            get
            {
                if (whatsOnList == null) whatsOnList = CinemaTable.GetCurrentCinema().WhatsOn;
                return whatsOnList;
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

        static MovieTable movieTable;
        public static MovieTable MovieTable
        {
            get
            {
                if (movieTable == null) movieTable = new MovieTable();
                return movieTable;
            }
        }
        #endregion
    }
}

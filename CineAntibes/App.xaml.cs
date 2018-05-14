using System;
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
		}

		protected override void OnStart()
		{
			// Handle when your app starts
			// apply the color theme
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
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
	}
}

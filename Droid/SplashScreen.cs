using Android.App;
using Android.Content;
using Android.OS;

namespace CineAntibes.Droid
{
	[Activity(Label = "CineAntibes", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your application here
			StartActivity(new Intent(this, typeof(MainActivity)));
			Finish();
		}

		public override void OnBackPressed()
		{
			// avoid back button on the splashscreen
		}
	}
}

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace CineAntibes.Droid
{
	[Activity(Label = "CineAntibes", Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
				Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
				Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#CA3C1F")); 
            }

            LoadApplication(new App());
        }
    }
}

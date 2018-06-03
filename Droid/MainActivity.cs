using System.Reflection;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;

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

            Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);

			var app = new App();

			#region theme color
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
				Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            }

            app.OnUseCustomTheme += (s, a) =>
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
					Color mainColor = (Color)Xamarin.Forms.Application.Current.Resources["appMainColor"];
					Color mainColorDark = (Color)Xamarin.Forms.Application.Current.Resources["appMainDarkColor"];
                    SetStatusBarColor(PCLToAndroidColor(mainColor));
                    Window.SetNavigationBarColor(PCLToAndroidColor(mainColorDark));

                    var prop = typeof(Color).GetRuntimeProperty(nameof(Color.Accent));
                    var setter = prop?.SetMethod;
                    // Invoke the setter method of the Color.Accent property to overwrite it with the custom accent color
					setter?.Invoke(Color.Accent, new object[] { (Color)Xamarin.Forms.Application.Current.Resources["appAccentColor"] });
                }
            };
            #endregion

			LoadApplication(app);
        }

		public static Android.Graphics.Color PCLToAndroidColor(Color color)
        {
            return Xamarin.Forms.Platform.Android.ColorExtensions.ToAndroid(color);
        }
    }
}

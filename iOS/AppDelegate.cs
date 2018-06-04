using System.Reflection;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace CineAntibes.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            CachedImageRenderer.Init();

            var cineApp = new App();

            cineApp.OnUseCustomTheme += (s, a) =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

                var prop = typeof(Color).GetRuntimeProperty(nameof(Color.Accent));
                var setter = prop?.SetMethod;
                // Invoke the setter method of the Color.Accent property to overwrite it with the custom accent color
                setter?.Invoke(Color.Accent, new object[] { (Color)Xamarin.Forms.Application.Current.Resources["appAccentColor"] });
            };


            LoadApplication(cineApp);

            return base.FinishedLaunching(app, options);
        }
    }
}

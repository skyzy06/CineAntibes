using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace CineAntibes.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}

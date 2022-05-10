using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ObjCRuntime;
using UIKit;

namespace DohrniiFoundation.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        private App _app;
        // This method is invoked when the application has loaded and is ready to run. In this s
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            AppCenter.Start("ios= 2feaa84d-4a81-404c-9c90-2b61c0a5eba9;", typeof(Analytics), typeof(Crashes));
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("MediaElement_Experimental", "IndicatorView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }       
        /// <summary>
        /// This method is used to set the app orientation
        /// </summary>
        /// <param name="application"></param>
        /// <param name="forWindow"></param>
        /// <returns></returns>
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow)
        {
            if (Xamarin.Forms.Application.Current == null || Xamarin.Forms.Application.Current.MainPage == null)
            {
              return UIInterfaceOrientationMask.Portrait;
            }
            else
            {
             return UIInterfaceOrientationMask.Portrait;               
            }
        }
    }
}

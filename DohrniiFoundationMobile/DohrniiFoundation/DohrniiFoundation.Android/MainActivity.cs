using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using Xamarin.Forms.Platform.Android.AppLinks;
using DohrniiFoundation.Helpers;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;

namespace DohrniiFoundation.Droid
{
    [Activity(Label = "DohrniiFoundation", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
     //REMARK: Deep Linking to reset the password
    [IntentFilter(new[] { Intent.ActionView },
                  DataScheme = "https",
                  DataHost = StringConstant.DeepLinkingAPIKeyURL,
                  DataPathPrefix = "/reset_password",
                  AutoVerify = true,
                  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable })]
    [IntentFilter(new[] { Intent.ActionView },
                  DataScheme = "http",
                  DataHost = StringConstant.DeepLinkingAPIKeyURL,
                  AutoVerify = true,
                  DataPathPrefix = "/reset_password",
                  Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {            
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            AppCenter.Start("android=67a7631f-e3bf-4014-bf07-7aa71c93e77a;", typeof(Analytics), typeof(Crashes));
            base.OnCreate(savedInstanceState);           
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            AndroidAppLinks.Init(this);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            CrossMedia.Current.Initialize();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
        }
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
            }
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }
    }
}
using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;

namespace DohrniiFoundation.Droid
{
    [Activity(Label = "DohrniiFoundation", Icon = "@mipmap/Icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        protected override void OnResume()
        {
            base.OnResume();
            System.Threading.Thread.Sleep(100);
            StartActivity(typeof(MainActivity));
        }
    }
}
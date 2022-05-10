using Android.App;
using Android.Content.PM;
using DohrniiFoundation.Droid.Services;
using DohrniiFoundation.IServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(OrientationService))]
namespace DohrniiFoundation.Droid.Services
{
    public class OrientationService : IOrientationService
    {
        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}
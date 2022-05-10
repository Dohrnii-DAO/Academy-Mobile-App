using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.User;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

[assembly: ExportFont("futur.ttf", Alias = "Futur")]
[assembly: ExportFont("Futura Bold font.ttf", Alias = "Bold")]
[assembly: ExportFont("Futura Book font.ttf", Alias = "Book")]
[assembly: ExportFont("FuturaPTDemi.otf", Alias = "PTDemi")]
[assembly: ExportFont("FuturaPTBook.otf", Alias = "PTBook")]
[assembly: ExportFont("FuturaMediumBT.ttf", Alias = "BTMedium")]
[assembly: ExportFont("MonumentExtended-Regular.otf", Alias = "MonumentRegular")]
[assembly: ExportFont("Poppins-Regular.ttf", Alias = "PoppinsRegular")]
[assembly: ExportFont("Poppins-SemiBold.ttf", Alias = "PoppinsSemiBold")]
[assembly: ExportFont("Poppins-Medium.ttf", Alias = "PoppinsMedium500")]
[assembly: ExportFont("Poppins-LightItalic.ttf", Alias = "PoppinsItalic300")]

namespace DohrniiFoundation
{
    public partial class App : Application
    {
        #region Constructor
        public App()
        {
            Device.SetFlags(new string[] { "MediaElement_Experimental", "IndicatorView_Experimental", "Markup_Experimental", "Brush_Experimental", "Shapes_Experimental" });
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
        #endregion

        #region Properties
        public static MasterDetailPage MasterDetail { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// On start to track the app is installed on device
        /// </summary>
        protected override void OnStart()
        {
            VersionTracking.Track();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        /// <summary>
        /// Method is used to get the app link to reset the password
        /// </summary>
        /// <param name="uri"></param>
        protected async override void OnAppLinkRequestReceived(Uri uri)
        {
            try
            {
                if (uri.Host.EndsWith(StringConstant.DeepLinkingAPIKeyURL, StringComparison.OrdinalIgnoreCase))
                {
                    if (uri != null && uri.Segments != null)
                    {
                        string[] uriString = uri.OriginalString.Split('=');
                        if (uri.Segments[1] == StringConstant.reset_password)
                        {
                            Preferences.Set(StringConstant.ResetPasswordToken, uriString[1]);
                            await Current.MainPage.Navigation.PushModalAsync(new ResetPasswordPage());
                            Preferences.Remove(DFResources.EmailText);
                            Preferences.Remove(DFResources.PasswordText);
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            base.OnAppLinkRequestReceived(uri);
        }
        #endregion
    }
}

using System;
using System.Diagnostics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        #region Constructor
        public LoginPage()
        {
            try
            {
                InitializeComponent();
                if (Debugger.IsAttached)
                {
                    emailEntry.Text = "sylvester@dohrnii.io";
                    passwordEntry.Text = "@Justme12";
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Method
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        #endregion
    }
}
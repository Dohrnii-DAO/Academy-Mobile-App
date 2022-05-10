using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Pages;
using System;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvertXPToJellyfishPage : PopupPage
    {
        public ConvertXPToJellyfishPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        /// <summary>
        /// Implement the funtionality to convert the XP to jellyfish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Entry_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            try
            {
              await VM.ConvertXPToJellyfish();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
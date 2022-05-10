using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonOnboardingPage : ContentPage
    {
        public LessonOnboardingPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        /// <summary>
        /// This method is to handle the next button text when items get scroll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onBoardingCarousel_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            try
            {
                VM.OnBoardingCurrentItemChanged();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
using Microsoft.AppCenter.Crashes;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DohrniiFoundation.Views.Lessons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonsPage : ContentPage
    {
        public LessonsPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetStatus();
        }
        private async Task GetStatus()
        {
            try
            {
                lessonsVM.IsLoading = true;
                await Task.Delay(100);
                await lessonsVM.GetUserStatus();
                await lessonsVM.GetChapterProgress();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
using DohrniiFoundation.Helpers;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.Socials
{
    public class TestingFontViewModel : ObservableObject
    {
        public ICommand BackBtnCommand { get; set; }

        public TestingFontViewModel()
        {
            BackBtnCommand = new Command(BackClick);
        }

        private async void BackClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}

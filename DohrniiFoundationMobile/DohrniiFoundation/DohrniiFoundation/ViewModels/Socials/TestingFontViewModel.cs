using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
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

        private HtmlWebViewSource htmlWebViewSource;
        private string classUrl;
        public ICommand BackBtnCommand { get; set; }

        public HtmlWebViewSource HtmlWebViewSource
        {
            get { return htmlWebViewSource; }
            set
            {
                if (htmlWebViewSource != value)
                {
                    htmlWebViewSource = value;
                    this.OnPropertyChanged(nameof(HtmlWebViewSource));
                }
            }
        }
        public string ClassUrl
        {
            get { return classUrl; }
            set
            {
                if (classUrl != value)
                {
                    classUrl = value;
                    this.OnPropertyChanged(nameof(ClassUrl));
                }
            }
        }

        public TestingFontViewModel()
        {
            BackBtnCommand = new Command(BackClick);
            ClassUrl = "https://lemon-pond-048be1410.1.azurestaticapps.net/chapter3/lesson3/class1.html";
            HtmlWebViewSource = new HtmlWebViewSource()
            {
                BaseUrl = DependencyService.Get<ILocalBaseUrl>().Get(),
                Html = Device.RuntimePlatform == Device.Android ? StringConstant.HtmlContentWithFont.Replace("[[fontpath]]","file:///android_asset/fonts/") : StringConstant.HtmlContentWithFont.Replace("[[fontpath]]","")
            };
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

using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.Views.User;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the verify email
    /// </summary> 
    public class VerifyMailViewModel : ObservableObject
    {
        #region properties
        public string Loginbg { get; set; } = StringConstant.Loginbg;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string Applogo { get; set; } = StringConstant.Applogo;
        public string Back { get; set; } = StringConstant.Back;
        public string emailverify { get; set; } = StringConstant.emailverify;
        public ICommand BackCommand { get; set; }
        public ICommand VerifyEmailCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize the object
        /// </summary>
        public VerifyMailViewModel()
        {
            try
            {
                BackCommand = new Command(BackClick);
                VerifyEmailCommand = new Command(VerifyEmailClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to pop to back 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private void BackClick()
        {
            try
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to pop to back 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private async void VerifyEmailClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushPopupAsync(new VerifiedMailPopUpPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}

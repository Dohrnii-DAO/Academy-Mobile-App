using DohrniiFoundation.Helpers;
using DohrniiFoundation.Resources;
using Microsoft.AppCenter.Crashes;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.More
{
    /// <summary>
    /// This view model is to bind and implement the functionality of the customer support
    /// </summary>
    public class CustomerSupportViewModel : ObservableObject
    {
        #region Public Properties
        public string EditorText { get; set; }
        public ICommand SendCommand { get; set; }
        #endregion

        #region Constructor
        public CustomerSupportViewModel()
        {
            try
            {
                SendCommand = new Command(SendClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is to send the message for the customer support
        /// </summary>
        private void SendClick()
        {
            try
            {
                if (string.IsNullOrEmpty(EditorText))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseentermessageText, DFResources.OKText);
                }
                else
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion
    }
}

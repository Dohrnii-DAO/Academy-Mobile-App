using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the forgot password 
    /// </summary> 
    public class ForgotPasswordViewModel : ObservableObject
    {

        #region Variables
        private string email;
        private static IAPIService aPIService;
        private ForgotPasswordRequestModel forgotPasswordRequestModel;
        private Color emailFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        #endregion

        #region Properties
        public string Mail { get; set; } = StringConstant.Mail;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public Color EmailFrameBorderColor
        {
            get { return emailFrameBorderColor; }
            set
            {
                emailFrameBorderColor = value;
                OnPropertyChanged(nameof(EmailFrameBorderColor));
            }
        }
        public ICommand SendCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the model 
        /// </summary> 
        public ForgotPasswordViewModel()
        {
            try
            {
                aPIService = new APIServices();
                forgotPasswordRequestModel = new ForgotPasswordRequestModel();
                SendCommand = new Command(SendEmailClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to send email to change password 
        /// </summary>
        /// <param name></param>      
        /// <returns></returns>
        private async void SendEmailClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {

                    if (await EmailValidation())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        forgotPasswordRequestModel = new ForgotPasswordRequestModel()
                        {
                            Email = Email.ToLower(),
                            AccessType = StringConstant.AccessType,
                        };
                        ResponseModel responseModel = await aPIService.ForgotPasswordService(forgotPasswordRequestModel);
                        if (responseModel != null)
                        {
                            if (responseModel.Status && responseModel.StatusCode == 200)
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, responseModel.Message, DFResources.OKText);
                                await Application.Current.MainPage.Navigation.PopPopupAsync();
                            }
                            else if (!responseModel.Status && responseModel.StatusCode == 202)
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, responseModel.Message, DFResources.OKText);
                            }
                            else
                            {
                                if (responseModel.StatusCode == 501 || responseModel.StatusCode == 401 || responseModel.StatusCode == 400 || responseModel.StatusCode == 404)
                                {
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, responseModel.Message, DFResources.OKText);
                                }
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.SomethingWrongText, DFResources.OKText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                IsLoading = false;
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }
        /// <summary>
        /// Method to check the email validation
        /// </summary>
        /// <returns></returns>
        private async Task<bool> EmailValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterEmail, DFResources.OKText);
                }
                else if (!string.IsNullOrEmpty(Email) && !Utilities.IsValidEmailAddress(Email.ToLower()))
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidEmail, DFResources.OKText);
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// This command to set border color of email and check validation of email 
        /// </summary>
        public Command EmailTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (string.IsNullOrEmpty(Email))
                    {
                        EmailFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    }
                    else
                    {
                        bool IsValid = Regex.IsMatch(Email, StringConstant.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                        EmailFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                    }
                });
            }
        }        
        #endregion
    }
}

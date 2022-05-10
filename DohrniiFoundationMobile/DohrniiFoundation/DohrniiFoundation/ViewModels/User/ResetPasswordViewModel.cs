using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the reset password  
    /// </summary> 
    public class ResetPasswordViewModel : ObservableObject
    {
        #region Variables 
        private string newPassword;
        private string confirmPassword;
        private static IAPIService aPIService;
        private ResetPasswordRequestModel resetPasswordRequestModel;
        private Color newPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color confirmPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        #endregion

        #region Properties
        public string Lock { get; set; } = StringConstant.Lock;
        public string NewPassword
        {
            get
            {
                return newPassword;
            }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color NewPasswordFrameBorderColor
        {
            get { return newPasswordFrameBorderColor; }
            set
            {
                newPasswordFrameBorderColor = value;
                OnPropertyChanged(nameof(NewPasswordFrameBorderColor));
            }
        }
        public Color ConfirmPasswordFrameBorderColor
        {
            get { return confirmPasswordFrameBorderColor; }
            set
            {
                confirmPasswordFrameBorderColor = value;
                OnPropertyChanged(nameof(ConfirmPasswordFrameBorderColor));
            }
        }
        public ICommand SaveCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize the object
        /// </summary>
        public ResetPasswordViewModel()
        {
            try
            {
                aPIService = new APIServices();
                this.resetPasswordRequestModel = new ResetPasswordRequestModel();
                SaveCommand = new Command(SaveClick);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to check the all password validation to save the reset password
        /// </summary>
        /// <returns></returns>
        private bool PasswordValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.AllMandatoryText, DFResources.OKText);
                }
                else if (!string.IsNullOrEmpty(NewPassword) && !Utilities.IsValidPassword(NewPassword))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.SignupPasswordValidationText.Replace("\\n", "\n"), DFResources.OKText);
                }
                else if (!string.IsNullOrEmpty(NewPassword) && NewPassword != ConfirmPassword)
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.NewAndConfirmPasswordSameText, DFResources.OKText);
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
        /// API implementation to save the reset password 
        /// </summary>
        private async void SaveClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (PasswordValidation())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        this.resetPasswordRequestModel = new ResetPasswordRequestModel()
                        {
                            Token = Preferences.Get(StringConstant.ResetPasswordToken, string.Empty),
                            Password = NewPassword,
                        };
                        ResponseModel responseModel = await aPIService.ResetPasswordService(resetPasswordRequestModel);
                        if (responseModel != null)
                        {
                            if (responseModel.Status && responseModel.StatusCode == 200)
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, responseModel.Message, DFResources.OKText);
                                await Application.Current.MainPage.Navigation.PopModalAsync();
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
        /// This command to set border color of password and check validation of password 
        /// </summary>
        public Command PasswordTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        NewPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    }
                    else
                    {
                        if (NewPassword.Contains(" "))
                        {
                            NewPassword = NewPassword.Replace(" ", string.Empty);
                        }
                        else
                        {
                            bool IsValid = Regex.IsMatch(NewPassword, StringConstant.passwordRegex);
                            NewPasswordFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                        }
                    }
                });
            }
        }
        /// <summary>
        /// This command to set border color of confirm password and check validation of confirm password 
        /// </summary>
        public Command ConfirmPasswordTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (!string.IsNullOrEmpty(NewPassword))
                    {
                        if (string.IsNullOrEmpty(ConfirmPassword))
                        {
                            ConfirmPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                        }
                        else
                        {
                            if (ConfirmPassword.Contains(" "))
                            {
                                ConfirmPassword = ConfirmPassword.Replace(" ", string.Empty);
                            }
                            else
                            {
                                bool IsNewValid = Regex.IsMatch(ConfirmPassword, StringConstant.passwordRegex);
                                if (IsNewValid)
                                {
                                    bool IsValid = NewPassword.Equals(ConfirmPassword);
                                    if (IsValid)
                                    {
                                        ConfirmPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                                    }
                                    else
                                    {
                                        ConfirmPasswordFrameBorderColor = (Color)Application.Current.Resources["RedText"];
                                    }
                                }
                                else
                                {
                                    ConfirmPasswordFrameBorderColor = (Color)Application.Current.Resources["RedText"];
                                }
                            }
                        }
                    }
                    else
                    {
                        ConfirmPassword = string.Empty;
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.EnterPasswordFirstText, DFResources.OKText);
                    }
                });
            }
        }
        #endregion
    }
}

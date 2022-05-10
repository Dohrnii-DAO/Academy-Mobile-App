using System;
using DohrniiFoundation.Helpers;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Xamarin.Forms;
using DohrniiFoundation.Resources;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Services;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.APIRequestModel.User;
using DohrniiFoundation.Views;
using System.Text.RegularExpressions;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the change password 
    /// </summary>  
    public class ChangePasswordViewModel : ObservableObject
    {
        #region Private Properties
        private string oldPassword;
        private string newPassword;
        private string confirmPassword;
        private IAPIService aPIService;
        private Color oldPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color newPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color confirmPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        #endregion

        #region Public Properties
        public string Lock { get; set; } = StringConstant.Lock;
        public string BackArrow { get; set; } = StringConstant.BackArrow;
        public string PasswordIcon { get; set; } = StringConstant.PasswordIcon;
        public string OldPassword
        {
            get
            {
                return oldPassword;
            }

            set
            {
                if (oldPassword != value)
                {
                    oldPassword = value;
                    this.OnPropertyChanged();
                }
            }
        }

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
                    this.OnPropertyChanged();
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
                    this.OnPropertyChanged();
                }
            }
        }

        public Color OldPasswordFrameBorderColor
        {
            get { return oldPasswordFrameBorderColor; }
            set
            {
                oldPasswordFrameBorderColor = value;
                OnPropertyChanged(nameof(OldPasswordFrameBorderColor));
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
        /// Initializes a new instance of the command 
        /// </summary>
        public ChangePasswordViewModel()
        {
            try
            {
                SaveCommand = new Command(SaveClick);
                aPIService = new APIServices();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to check the all password validation
        /// </summary>
        /// <returns></returns>
        private bool PasswordValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(OldPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.AllMandatoryText, DFResources.OKText);
                }
                else if ((!string.IsNullOrEmpty(OldPassword) && !Utilities.IsValidPassword(OldPassword)) || (!string.IsNullOrEmpty(NewPassword) && !Utilities.IsValidPassword(NewPassword)) || !string.IsNullOrEmpty(ConfirmPassword) && !Utilities.IsValidPassword(ConfirmPassword))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidPassword, DFResources.OKText);
                }
                else if (!string.IsNullOrEmpty(NewPassword) && newPassword != ConfirmPassword)
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
        /// Method to save the changed password using change password API
        /// </summary>
        private async void SaveClick()
        {
            try
            {
                if (PasswordValidation())
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    ChangePasswordRequestModel changePasswordRequestModel = new ChangePasswordRequestModel()
                    {
                        OldPassword = OldPassword,
                        NewPassword = NewPassword
                    };
                    ResponseModel responseModel = await aPIService.ChangePasswordService(changePasswordRequestModel);
                    if (responseModel != null)
                    {
                        if (responseModel.Status && responseModel.StatusCode == 200)
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, responseModel.Message, DFResources.OKText);
                            Application.Current.MainPage = new LoginPage();
                        }
                        else if (!responseModel.Status && (responseModel.StatusCode == 202 || responseModel.StatusCode == 400))
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, responseModel.Message, DFResources.OKText);
                        }
                        else
                        {
                            if (responseModel.StatusCode == 501 || responseModel.StatusCode == 401 || responseModel.StatusCode == 404)
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
        /// This command to set border color of old password and check validation of old password
        /// </summary>
        public Command OldPasswordTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (string.IsNullOrEmpty(OldPassword))
                    {
                        OldPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    }
                    else
                    {
                        if (OldPassword.Contains(" "))
                        {
                            OldPassword = OldPassword.Replace(" ", string.Empty);
                        }
                        else
                        {
                            bool IsValid = Regex.IsMatch(OldPassword, StringConstant.passwordRegex);
                            OldPasswordFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                        }
                    }
                });
            }
        }
        /// <summary>
        /// This command to set border color of new password and check validation of new password 
        /// </summary>
        public Command NewPasswordTextChangedCommand
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
        /// This command is to set the border color of the confirm password text entry
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
                                bool IsValid = NewPassword.Equals(ConfirmPassword);
                                ConfirmPasswordFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                            }
                        }
                    }
                    else
                    {
                        ConfirmPassword = string.Empty;
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.EnterNewPasswordFirstText, DFResources.OKText);
                    }
                });
            }
        }
        #endregion
    }
}

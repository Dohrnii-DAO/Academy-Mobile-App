using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.User;
using Microsoft.AppCenter.Crashes;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the sign up 
    /// </summary> 
    public class SignUpViewModel : ObservableObject
    {
        #region Variables
        private string firstName;
        private string lastName;
        private string userName;
        private string email;
        private string password;
        private string confirmPassword;
        private double? contactNumber;
        private bool isPassword = true;
        private string checkBoxImage = StringConstant.UncheckBox;
        private static IAPIService aPIService;
        private SignUpRequestModel signUpRequestModel;
        private Color emailFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color userNameFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color passwordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color confirmPasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        #endregion

        #region Properties
        public string Loginbg { get; set; } = StringConstant.Loginbg;
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string Applogo { get; set; } = StringConstant.Applogo;
        public string Back { get; set; } = StringConstant.Back;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged();
                }
            }
        }
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
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
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
        public double? ContactNumber
        {
            get
            {
                return contactNumber;
            }
            set
            {
                if (contactNumber != value)
                {
                    contactNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsPassword
        {
            get
            {
                return isPassword;
            }
            set
            {
                if (isPassword != value)
                {
                    isPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CheckBoxImage
        {
            get
            {
                return checkBoxImage;
            }
            set
            {
                if (checkBoxImage != value)
                {
                    checkBoxImage = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsCheckBoxClicked = false;
        public Color EmailFrameBorderColor
        {
            get { return emailFrameBorderColor; }
            set
            {
                emailFrameBorderColor = value;
                OnPropertyChanged(nameof(EmailFrameBorderColor));
            }
        }
        public Color UserNameFrameBorderColor
        {
            get { return userNameFrameBorderColor; }
            set
            {
                userNameFrameBorderColor = value;
                OnPropertyChanged(nameof(UserNameFrameBorderColor));
            }
        }
        public Color PasswordFrameBorderColor
        {
            get { return passwordFrameBorderColor; }
            set
            {
                passwordFrameBorderColor = value;
                OnPropertyChanged(nameof(PasswordFrameBorderColor));
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
        public ICommand BackCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        public ICommand CheckBoxCommand { get; set; }
        public ICommand SignUpWithEmailCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialize the objects
        /// </summary>
        public SignUpViewModel()
        {
            try
            {
                BackCommand = new Command(BackClick);
                SignUpCommand = new Command(SignUpClick);
                SignInCommand = new Command(SignInClick);
                CheckBoxCommand = new Command(CheckBoxClick);
                SignUpWithEmailCommand = new Command(SignUpWithEmailClick);
                aPIService = new APIServices();
                signUpRequestModel = new SignUpRequestModel();
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
        private async void BackClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (FormatException ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to click on sign with email 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private async void SignUpWithEmailClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new SignUpPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to click on checkbox and handle password secure functionality
        /// </summary>
        private async void CheckBoxClick()
        {
            try
            {
                if (!IsCheckBoxClicked)
                {
                    IsPassword = false;
                    IsCheckBoxClicked = true;
                    CheckBoxImage = StringConstant.CheckBox;
                }
                else
                {
                    IsPassword = true;
                    IsCheckBoxClicked = false;
                    CheckBoxImage = StringConstant.UncheckBox;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        /// <summary>
        /// Method to signup the user with all required validation and API implementation
        /// </summary>
        private async void SignUpClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (SignUpValidation())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        signUpRequestModel = new SignUpRequestModel()
                        {
                            Firstname = string.IsNullOrEmpty(FirstName) ? string.Empty : FirstName,
                            Lastname = string.IsNullOrEmpty(LastName) ? string.Empty : LastName,
                            Email = Email.ToLower(),
                            AccessType = StringConstant.AccessType,
                            Password = Password,
                            Username = UserName,
                            Phone = ContactNumber > 0 ? ContactNumber : null,
                        };
                        ResponseModel responseModel = await aPIService.SignUpService(signUpRequestModel);
                        if (responseModel != null)
                        {
                            if (responseModel.Status && responseModel.StatusCode == 200)
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, responseModel.Message, DFResources.OKText);
                                Application.Current.MainPage = new LoginPage();
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
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.OopsText, DFResources.NoInternetConnectionText, DFResources.OKText);
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
        private async void SignInClick()
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
        /// <summary>
        /// Method to check the all the sign up fields
        /// </summary>
        /// <returns></returns>
        private bool SignUpValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterUserNameText, DFResources.OKText);
                }
                else
                {
                    if (!string.IsNullOrEmpty(UserName) && !Regex.IsMatch(UserName, StringConstant.UserNameRegex))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.UserWithOutNumberText, DFResources.OKText);
                    }
                    else if (string.IsNullOrEmpty(Email))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterEmail, DFResources.OKText);
                    }
                    else if (string.IsNullOrEmpty(Password))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterPassword, DFResources.OKText);
                    }
                    else if (string.IsNullOrEmpty(ConfirmPassword))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterConfirmPasswordText, DFResources.OKText);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Email) && (!Utilities.IsValidEmailAddress(Email.ToLower())))
                        {
                            Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidEmail, DFResources.OKText);
                        }
                        else if (!string.IsNullOrEmpty(Password) && !Utilities.IsValidPassword(Password))
                        {
                            Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.SignupPasswordValidationText.Replace("\\n", "\n"), DFResources.OKText);
                        }
                        else if (!string.IsNullOrEmpty(ConfirmPassword) && Password != ConfirmPassword)
                        {
                            if (!Utilities.IsValidPassword(ConfirmPassword))
                            {
                                Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PasswordAndConfirmPasswordSameText, DFResources.OKText);
                            }
                            else
                            {
                                Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PasswordAndConfirmPasswordSameText, DFResources.OKText);
                            }
                        }
                        else
                        {
                            result = true;
                        }
                    }
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
                        if (Email.Contains(" "))
                        {
                            Email = Email.Replace(" ", string.Empty);
                        }
                        else
                        {
                            bool IsValid = Regex.IsMatch(Email, StringConstant.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                            EmailFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                        }
                    }
                });
            }
        }
        /// <summary>
        /// This command to set border color of user name and check validation of user name 
        /// </summary>
        public Command UserNameTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (string.IsNullOrEmpty(UserName))
                    {
                        UserNameFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    }
                    else
                    {
                        if (UserName.Contains(" "))
                        {
                            UserName = UserName.Replace(" ", string.Empty);
                        }
                        else
                        {
                            bool IsValid = Regex.IsMatch(UserName, StringConstant.UserNameRegex);
                            if (IsValid)
                            {
                                UserNameFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                            }
                            else
                            {
                                UserNameFrameBorderColor = (Color)Application.Current.Resources["RedText"];
                            }
                        }
                    }
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

                    if (string.IsNullOrEmpty(Password))
                    {
                        PasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
                    }
                    else
                    {
                        if (Password.Contains(" "))
                        {
                            Password = Password.Replace(" ", string.Empty);
                        }
                        else
                        {
                            bool IsValid = Regex.IsMatch(Password, StringConstant.passwordRegex);
                            PasswordFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
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

                    if (!string.IsNullOrEmpty(Password))
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
                                    bool IsValid = Password.Equals(ConfirmPassword);
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

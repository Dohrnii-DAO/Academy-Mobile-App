using System;
using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIResponseModels.User;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.User;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using DohrniiFoundation.Views.Lessons;
using System.Text.RegularExpressions;

namespace DohrniiFoundation.ViewModels.User
{
    /// <summary>
    /// View model to binding and handle functionality of the login
    /// </summary>
    public class LoginViewModel : ObservableObject
    {
        #region Private Properties    
        private string rememeberImage = StringConstant.Unselected;
        private string email;
        private string password;
        private string emailAddressText = DFResources.EmailAddressText;
        private string passwordLabelText = DFResources.PasswordText;
        private string loginTitle = DFResources.PleaseLoginText;
        private bool isSignUpClicked = false;
        private bool isLoginVisible = true;
        private bool isLoginBtnVisible = true;
        private bool isSignUpBtnVisible = false;
        private string signText = DFResources.SignUpText;
        private string accountText = DFResources.AlreadyHaveAccountText;
        LoginRequestModel loginRequestModel;
        private static IAPIService aPIService;
        private Color emailFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color passwordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];        
        #endregion

        #region Properties
        public bool Isselected = false;
        public bool isPassword = true;
        public string EncryptedPassword { get; set; }
        public string DecryptedPassword { get; set; }
        public string AppIcon { get; set; } = StringConstant.AppIcon;
        public string Applogo { get; set; } = StringConstant.Applogo;
        public string Facebook { get; set; } = StringConstant.Facebook;
        public string Insta { get; set; } = StringConstant.Insta;
        public string Twitter { get; set; } = StringConstant.Twitter;
        public string Loginbg { get; set; } = StringConstant.Loginbg;
        public string EmailIcon { get; set; } = StringConstant.EmailIcon;
        public string PasswordIcon { get; set; } = StringConstant.PasswordIcon;
        public string Google { get; set; } = StringConstant.Google;
        public string Fb { get; set; } = StringConstant.Fb;
        public string Apple { get; set; } = StringConstant.Apple;
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
                    this.OnPropertyChanged();
                }
            }
        }
        public string RememeberImage
        {
            get
            {
                return rememeberImage;
            }
            set
            {
                if (rememeberImage != value)
                {
                    rememeberImage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string EmailAddressText
        {
            get
            {
                return emailAddressText;
            }
            set
            {
                if (emailAddressText != value)
                {
                    emailAddressText = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string PasswordLabelText
        {
            get
            {
                return passwordLabelText;
            }
            set
            {
                if (passwordLabelText != value)
                {
                    passwordLabelText = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsSignUpClicked
        {
            get
            {
                return isSignUpClicked;
            }
            set
            {
                if (isSignUpClicked != value)
                {
                    isSignUpClicked = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string LoginTitle
        {
            get
            {
                return loginTitle;
            }
            set
            {
                if (loginTitle != value)
                {
                    loginTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public bool IsLoginVisible
        {
            get
            {
                return isLoginVisible;
            }
            set
            {
                if (isLoginVisible != value)
                {
                    isLoginVisible = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string SignText
        {
            get
            {
                return signText;
            }
            set
            {
                if (signText != value)
                {
                    signText = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsLoginBtnVisible
        {
            get
            {
                return isLoginBtnVisible;
            }
            set
            {
                if (isLoginBtnVisible != value)
                {
                    isLoginBtnVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsSignUpBtnVisible
        {
            get
            {
                return isSignUpBtnVisible;
            }
            set
            {
                if (isSignUpBtnVisible != value)
                {
                    isSignUpBtnVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AccountText
        {
            get
            {
                return this.accountText;
            }
            set
            {
                if (this.accountText != value)
                {
                    this.accountText = value;
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
        public Color EmailFrameBorderColor
        {
            get { return emailFrameBorderColor; }
            set
            {
                emailFrameBorderColor = value;
                OnPropertyChanged(nameof(EmailFrameBorderColor));
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
        public ICommand LoginCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            try
            {
                LoginCommand = new Command(LoginClick);
                SignInCommand = new Command(SignInClick);
                ForgotPasswordCommand = new Command(ForgotPasswordClick);
                loginRequestModel = new LoginRequestModel();
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
        /// Method to login using the validation and API implementation
        /// </summary>
        private async void LoginClick()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (LoginValidation())
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = true;
                        });
                        loginRequestModel = new LoginRequestModel()
                        {
                            Email = Email.ToLower(),
                            Password = Password,
                            AccessType = StringConstant.AccessType,
                        };
                        LoginResponseModel responseModel = await aPIService.LoginService(loginRequestModel);
                        if (responseModel != null)
                        {
                            if (responseModel.StatusCode == 200 && responseModel.Success)
                            {
                                Preferences.Set("Email", Email);
                                Preferences.Set("Password", Password);
                                Utilities.UploadedProfileImage = null;
                                Preferences.Set(StringConstant.TabIdKey, 2);
                                Utilities.ApiCallTime = DateTime.UtcNow.AddSeconds(responseModel.Expires_in);
                                Utilities.BearerToken = responseModel.Token;
                                Preferences.Set(DFResources.AccessTokenText, responseModel.Token);
                                Preferences.Set(DFResources.AccessTokenExpiryText, responseModel.Expires_in);
                                UserDetailsApi();
                                //REMARK: Handle condition to navigate on Lessons Onboarding screen for new user
                                if (responseModel.IsNewUser == 1 )
                                {
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new LessonOnboardingPage());
                                }
                                else
                                {
                                    await Application.Current.MainPage.Navigation.PushModalAsync(new LessonsPage());
                                }
                            }
                            else if (!responseModel.Success && responseModel.StatusCode == 202)
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
        /// This method is to navigate to sign up page if user have not registered
        /// </summary>
        private async void SignInClick()
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
        /// This method is used to get the user profile details and user strategy assigned
        /// </summary>
        public async void UserDetailsApi()
        {
            try
            {
                IsLoading = true;
                //REMARK: Get the strategy assigned for the user if questionnaire is given
                StrategyStatusResponseModel strategiesResponseModel = await aPIService.GetUserStatusService();
                if (strategiesResponseModel != null)
                {
                    if (strategiesResponseModel.StatusCode == 200 && strategiesResponseModel.Status)
                    {
                        foreach (var userDetail in strategiesResponseModel.UserStatusDetails)
                        {
                            Preferences.Set(StringConstant.AssignedStrategyId, userDetail.AssignedStrategyId);
                        }
                    }
                }
                else
                {
                    Preferences.Set(StringConstant.AssignedStrategyId, 0);
                }
                //REMARK: Get the user details for the questionnaire
                UserProfileResponseModel userProfileResponseModel = await aPIService.GetUserProfileService();
                if (userProfileResponseModel != null)
                {
                    if (userProfileResponseModel.StatusCode == 200 && userProfileResponseModel.Status)
                    {
                        Preferences.Set(StringConstant.UserId, userProfileResponseModel.UserDetail.UserId);
                        Preferences.Set(StringConstant.QuestionnaireTaken, userProfileResponseModel.UserDetail.QuestionnaireTaken);
                        Preferences.Set(StringConstant.RetakeRequired, userProfileResponseModel.UserDetail.RetakeRequired);
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
                IsLoading = false;
            }
        }

        /// <summary>
        /// Method to click on forgot password 
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        private async void ForgotPasswordClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new ForgotPasswordPage());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// Method to check the email and password 
        /// </summary>
        /// <returns></returns>
        private bool LoginValidation()
        {
            bool result = false;
            try
            {
                if (string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterEmail, DFResources.OKText);

                }
                else if (string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Email))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterPassword, DFResources.OKText);
                }
                else if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterEmailAndPassword, DFResources.OKText);
                }
                else
                {
                    if (!Utilities.IsValidEmailAddress(Email.ToLower()) && Utilities.IsValidPassword(Password))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidEmail, DFResources.OKText);
                    }
                    else if (Utilities.IsValidEmailAddress(Email.ToLower()) && !Utilities.IsValidPassword(Password))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidPassword, DFResources.OKText);
                    }
                    else if (!Utilities.IsValidEmailAddress(Email.ToLower()) && !Utilities.IsValidPassword(Password))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidEmailAndPassword, DFResources.OKText);
                    }
                    else
                    {
                        if (Utilities.IsValidEmailAddress(Email.ToLower()) && Utilities.IsValidPassword(Password))
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
                        bool IsValid = Regex.IsMatch(Email, StringConstant.EmailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                        EmailFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
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
        #endregion
    }
}

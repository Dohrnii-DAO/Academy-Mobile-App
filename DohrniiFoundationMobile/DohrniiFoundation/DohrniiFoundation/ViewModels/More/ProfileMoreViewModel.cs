using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.APIResponseModels.User;
using DohrniiFoundation.Models.More;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.ViewModels.Lessons;
using DohrniiFoundation.Views;
using DohrniiFoundation.Views.More;
using DohrniiFoundation.Views.User;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.ViewModels.More
{
    /// <summary>
    /// This view model to bind the details of the user and profile more list
    /// </summary>
    public class ProfileMoreViewModel : ObservableObject
    {
        #region Private Properties
        private ObservableCollection<ProfileMoreModel> profileMoreList;
        private IAPIService aPIService;
        private string userName;
        private string userEmail;
        private string profileImage;
        private Command commandEditProfile;
        #endregion

        #region Public Properties
        public ObservableCollection<ProfileMoreModel> ProfileMoreList
        {
            get { return profileMoreList; }
            set
            {
                if (profileMoreList != value)
                {
                    profileMoreList = value;
                    OnPropertyChanged(nameof(ProfileMoreList));
                }
            }
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }
        #endregion

        #region Constructor
        public ProfileMoreViewModel()
        {
            try
            {
                aPIService = new APIServices();
                GetProfileDetails();
                GetProfileList();
                MessagingCenter.Subscribe<EditProfileViewModel, bool>(this, StringConstant.UpdateProfileRefresh, async (s, e) =>
                {
                    await GetProfileDetails();
                });
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is get all the user details by integrating the API
        /// </summary>
        public async Task GetProfileDetails()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = true;
                });
                UserProfileResponseModel userProfileResponseModel = await aPIService.GetUserProfileService();
                if (userProfileResponseModel != null)
                {
                    if (userProfileResponseModel.StatusCode == 200 && userProfileResponseModel.Status)
                    {
                        UserName = userProfileResponseModel.UserDetail.UserName;
                        UserEmail = userProfileResponseModel.UserDetail.Email;
                        Utilities.UserName = UserName;
                        if (userProfileResponseModel.UserDetail.ProfileImage != null)
                        {
                            ProfileImage = null;
                            ProfileImage = userProfileResponseModel.UserDetail.ProfileImage;
                            Utilities.UploadedProfileImage = ProfileImage;
                        }
                    }
                    else if (!userProfileResponseModel.Status && userProfileResponseModel.StatusCode == 202)
                    {
                        await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, userProfileResponseModel.Message, DFResources.OKText);
                    }
                    else
                    {
                        if (userProfileResponseModel.StatusCode == 501 || userProfileResponseModel.StatusCode == 401 || userProfileResponseModel.StatusCode == 400 || userProfileResponseModel.StatusCode == 404)
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, userProfileResponseModel.Message, DFResources.OKText);
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.SomethingWrongText, DFResources.OKText);
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
        /// This method is to get the profile list items
        /// </summary>
        private void GetProfileList()
        {
            try
            {
                ProfileMoreList = new ObservableCollection<ProfileMoreModel>()
                {
                    new ProfileMoreModel()
                    {
                        Title = DFResources.ChangePasswordText,
                        TitleImage = StringConstant.LockIcon,
                        IsNextVisible = true,
                    },
                    new ProfileMoreModel()
                    {
                        Title = DFResources.BiometricDetailsText,
                        TitleImage = StringConstant.FingerPrint,
                        IsNextVisible = true,
                    },
                    new ProfileMoreModel()
                    {
                        Title = DFResources.FAQText,
                        TitleImage = StringConstant.FaqIcon,
                        IsNextVisible = true,
                    },
                    new ProfileMoreModel()
                    {
                        Title = DFResources.CustomerSupportText,
                        TitleImage = StringConstant.HeadphonesIcon,
                        IsNextVisible = true,
                    },
                    new ProfileMoreModel()
                    {
                        Title = DFResources.PrivacyPolicyText,
                        TitleImage = StringConstant.PolicyIcon,
                        IsNextVisible = true,
                    },
                    new ProfileMoreModel()
                    {
                        Title = DFResources.LogoutText,
                        TitleImage = StringConstant.LogoutIcon,
                        IsNextVisible = false,
                    },
                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to select the profile list item from the list
        /// </summary>
        /// <param name="selectedItem"></param>
        public async void GetSelectedItem(ProfileMoreModel selectedItem)
        {
            try
            {
                if (selectedItem != null)
                {
                    if (selectedItem.Title == DFResources.ChangePasswordText)
                    {
                        await Application.Current.MainPage.Navigation.PushModalAsync(new ChangePasswordPage());
                    }
                    else if (selectedItem.Title == DFResources.CustomerSupportText)
                    {
                    }
                    else if (selectedItem.Title == DFResources.LogoutText)
                    {
                        bool result = await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.LogoutAlertText, DFResources.YesText, DFResources.NoText);
                        if (result)
                        {
                            Utilities.UploadedProfileImage = null;
                            await LogoutApp();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        ///  Method to logout from app and API implementation
        /// </summary>
        /// <returns></returns>
        public async Task LogoutApp()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    ResponseModel responseModel = await aPIService.LogoutService();
                    if (responseModel != null)
                    {
                        if (responseModel.StatusCode == 200 && responseModel.Status)
                        {
                            Preferences.Remove(DFResources.EmailText);
                            Preferences.Remove(DFResources.PasswordText);
                            Preferences.Remove(DFResources.AccessTokenText);
                            Preferences.Remove(DFResources.AccessTokenExpiryText);
                            MessagingCenter.Unsubscribe<ClassCompleteViewModel>(this, StringConstant.UpdateClassesApi);
                            MessagingCenter.Unsubscribe<ConvertXPToJellyfishViewModel>(this, StringConstant.UpdateUserXPRefresh);
                            MessagingCenter.Unsubscribe<EditProfileViewModel>(this, StringConstant.UpdateProfileRefresh);                            
                            Application.Current.MainPage = new LoginPage();
                            await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, responseModel.Message, DFResources.OKText);
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
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// This Command is used for the edit profile button.
        /// </summary>
        public Command CommandEditProfile
        {
            get
            {
                if (commandEditProfile == null)
                    commandEditProfile = new Command(() =>
                    {
                        try
                        {
                            Application.Current.MainPage.Navigation.PushModalAsync(new EditProfilePage());
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.Source);
                        }
                    });
                return commandEditProfile;
            }
        }
        #endregion
    }
}

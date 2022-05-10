using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel.User;
using DohrniiFoundation.Models.APIResponseModels.User;
using DohrniiFoundation.Resources;
using DohrniiFoundation.Services;
using DohrniiFoundation.Views;
using Plugin.Permissions;
using Microsoft.AppCenter.Crashes;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;

namespace DohrniiFoundation.ViewModels.Lessons
{
    /// <summary>
    /// This view model is to bind the edit profile UI with functionalities
    /// </summary>
    public class EditProfileViewModel : ObservableObject
    {
        #region Private Properties
        private string userName;
        private string password;
        private static IAPIService aPIService;
        private ImageSource profileImage;
        private string profileImageString;
        private Color changePasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        private Color userNameFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
        #endregion

        #region Public Properties
        public string ProfileImageBase64String = string.Empty;
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
                    OnPropertyChanged(nameof(UserName));
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
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public ImageSource ProfileImage
        {
            get { return profileImage; }
            set
            {
                profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }
        public string ProfileImageString
        {
            get { return profileImageString; }
            set
            {
                profileImageString = value;
                OnPropertyChanged(nameof(ProfileImageString));
            }
        }
        public Color ChangePasswordFrameBorderColor
        {
            get { return changePasswordFrameBorderColor; }
            set {
                changePasswordFrameBorderColor = value;
                OnPropertyChanged(nameof(ChangePasswordFrameBorderColor)); }
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
        public ICommand SaveCommand { get; set; }
        public ICommand CameraCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This constructor is to initialize the objects
        /// </summary>
        public EditProfileViewModel()
        {
            try
            {
                SaveCommand = new Command(SaveClick);
                aPIService = new APIServices();
                CameraCommand = new Command(CameraClick);
                if (!string.IsNullOrEmpty(Utilities.UserName))
                {
                    UserName = Utilities.UserName;
                }
                if (Utilities.UploadedProfileImage == null)
                {
                    ProfileImage = null;
                    ProfileImageBase64String = string.Empty;
                }
                else
                {
                    GetBase64UsingUrl();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is to get the base64 of the image from profile image url
        /// </summary>
        private void GetBase64UsingUrl()
        {
            try
            {
                string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(Utilities.UploadedProfileImage));
                using (var client = new WebClient())
                {
                    byte[] dataBytes = client.DownloadData(new Uri(Utilities.UploadedProfileImage.ToString()));
                    string encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    ProfileImageBase64String = StringConstant.UpdateProfilePrefix + encodedFileAsBase64;
                    ProfileImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(encodedFileAsBase64)));
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is used to update the user profile using API 
        /// </summary>
        private async void SaveClick()
        {
            try
            {
                if (SaveProfileValidation())
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        IsLoading = true;
                    });
                    UpdateProfileRequestModel changePasswordRequestModel = new UpdateProfileRequestModel()
                    {
                        Username = UserName,
                        Password = Password,
                        ProfileImage = ProfileImageBase64String
                    };
                    int userId = Preferences.Get(StringConstant.UserId, 0) == 0 ? 0 : Preferences.Get(StringConstant.UserId, 0);
                    UpdateProfileResponseModel updateProfileResponseModel = await aPIService.UpdateProfileService(changePasswordRequestModel, userId);
                    if (updateProfileResponseModel != null)
                    {
                        if (updateProfileResponseModel.Status && updateProfileResponseModel.StatusCode == 200)
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.SuccessText, updateProfileResponseModel.Message, DFResources.OKText);
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                            MessagingCenter.Send<EditProfileViewModel, bool>(this, StringConstant.UpdateProfileRefresh, true);
                        }
                        else if (!updateProfileResponseModel.Status && updateProfileResponseModel.StatusCode == 202)
                        {
                            await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, updateProfileResponseModel.Message, DFResources.OKText);
                        }
                        else
                        {
                            if (updateProfileResponseModel.StatusCode == 501 || updateProfileResponseModel.StatusCode == 401 || updateProfileResponseModel.StatusCode == 400 || updateProfileResponseModel.StatusCode == 404)
                            {
                                await Application.Current.MainPage.Navigation.PushModalAsync(new ResponseErrorPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, updateProfileResponseModel.Message, DFResources.OKText);
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
        /// This method is used to handle the validation of the edit profile
        /// </summary>
        /// <returns></returns>
        private bool SaveProfileValidation()
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
                    else if (string.IsNullOrEmpty(Password))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterPassword, DFResources.OKText);
                    }
                    else if (!string.IsNullOrEmpty(Password) && !Regex.IsMatch(Password, StringConstant.passwordRegex))
                    {
                        Application.Current.MainPage.DisplayAlert(DFResources.AlertText, DFResources.PleaseEnterValidPassword, DFResources.OKText);
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return result;
        }
        /// <summary>
        /// This method is used to update the user profile using API 
        /// </summary>
        private async void CameraClick()
        {
            try
            {
                string action = await Application.Current.MainPage.DisplayActionSheet(DFResources.SelectOptionText, DFResources.CancelText, null, DFResources.CameraText, DFResources.GalleryText);
                if (action == DFResources.CameraText)
                {
                    await TakePhoto();
                }
                else if (action == DFResources.GalleryText)
                {
                    await SelectPhoto();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
        /// <summary>
        /// This method is to implement the functionality to upload profile photo from camera
        /// </summary>
        /// <returns></returns>
        public async Task<ImageSource> TakePhoto()
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable ||
                        !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.NoCameraText, DFResources.SorryNocameraavailableText, DFResources.OKText);
                    return null;
                }

                bool isPermissionGranted = await RequestCameraAndGalleryPermissions();
                if (!isPermissionGranted)
                    return null;

                MediaFile File = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = false,
                    CompressionQuality = 25,
                    RotateImage = true,
                    PhotoSize = PhotoSize.Small,
                    AllowCropping = false,
                    SaveMetaData = false,
                    DefaultCamera = CameraDevice.Front,
                    Directory = StringConstant.Symbol,
                });

                if (File == null)
                    return null;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    File.GetStream().CopyTo(memoryStream);
                    byte[] userProfileImagebytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(userProfileImagebytes);
                    ProfileImageBase64String = StringConstant.UpdateProfilePrefix + base64String;
                    ProfileImage = ImageSource.FromStream(() => new MemoryStream(userProfileImagebytes));
                    Utilities.UploadedBase64Image = ProfileImageBase64String;
                    File.Dispose();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return ProfileImage;
        }
        /// <summary>
        /// This method is to implement the functionality to upload profile photo from gallery
        /// </summary>
        /// <returns></returns>
        public async Task<ImageSource> SelectPhoto()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert(DFResources.PhotosNotSupportedText, DFResources.SorryPermissionnotgrantedtophotosText, DFResources.OKText);
                    return null;
                }

                bool isPermissionGranted = await RequestCameraAndGalleryPermissions();
                if (!isPermissionGranted)
                    return null;

                MediaFile File = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });

                if (File == null)
                    return null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    File.GetStream().CopyTo(memoryStream);
                    byte[] userProfileImagebytes = memoryStream.ToArray();
                    string base64String = Convert.ToBase64String(userProfileImagebytes);
                    ProfileImageBase64String = StringConstant.UpdateProfilePrefix + base64String;
                    ProfileImage = ImageSource.FromStream(() => new MemoryStream(userProfileImagebytes));
                    Utilities.UploadedBase64Image = ProfileImageBase64String;
                    File.Dispose();
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return ProfileImage;
        }
        /// <summary>
        /// This method is to request permissions for camera, gallery and storage
        /// </summary>
        /// <returns></returns>
        private async Task<bool> RequestCameraAndGalleryPermissions()
        {
            try
            {
                PermissionStatus cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                PermissionStatus storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                PermissionStatus photosStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    System.Collections.Generic.Dictionary<Permission, PermissionStatus> permissionRequestResult = await CrossPermissions.Current.RequestPermissionsAsync(
                        new Permission[] { Permission.Camera, Permission.Storage, Permission.Photos });

                    PermissionStatus cameraResult = permissionRequestResult[Plugin.Permissions.Abstractions.Permission.Camera];
                    PermissionStatus storageResult = permissionRequestResult[Plugin.Permissions.Abstractions.Permission.Storage];
                    PermissionStatus photosResult = permissionRequestResult[Plugin.Permissions.Abstractions.Permission.Photos];

                    return (
                        cameraResult != PermissionStatus.Denied &&
                        storageResult != PermissionStatus.Denied &&
                        photosResult != PermissionStatus.Denied);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return true;
        }        
        /// <summary>
        /// This command is to set the border color of the password text entry
        /// </summary>
        public Command PasswordTextChangedCommand
        {
            get
            {
                return new Command(() =>
                {

                    if (string.IsNullOrEmpty(Password))
                    {
                        ChangePasswordFrameBorderColor = (Color)Application.Current.Resources["LessonXPFirstColor"];
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
                            ChangePasswordFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                        }
                    }
                });
            }
        }
        /// <summary>
        /// This command is to set the border color of the usre name text entry
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
                            UserNameFrameBorderColor = IsValid ? (Color)Application.Current.Resources["LessonXPFirstColor"] : (Color)Application.Current.Resources["RedText"];
                        }
                    }
                });
            }
        }
        
        #endregion
    }
}

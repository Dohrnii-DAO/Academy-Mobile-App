using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.Socials;
using DohrniiFoundation.Models.UserModels;
using DohrniiFoundation.Resources;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DohrniiFoundation.Services
{
    public class UserService : IUserService
    {
        #region Private Members
        private static ServiceHelpers serviceHelpers;
        #endregion

        #region Constructor
        public UserService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        #endregion

        public async Task<LoginResponse> Login(LoginRequestModel loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(loginRequest);
                ResponseModel response = await serviceHelpers.PostRequestAsync(serializedRequest, StringConstant.LoginEndPoint, false, null);
                loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Data);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return loginResponse;
        }

        public async Task<List<AppUser>> GetUsers()
        {
            List<AppUser> users = new List<AppUser>();
            try
            {
                ResponseModel response = await serviceHelpers.GetRequestAsync(StringConstant.UsersEndPoint, false, Utilities.AccessToken);
                if (response.IsSuccess)
                {
                    users = JsonConvert.DeserializeObject<List<AppUser>>(response.Data);
                }
                else
                {
                    var msg = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, msg.Detail, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return users;
        }

        public async Task<AppUser> GetUsers(int id)
        {
            AppUser user = null;
            try
            {
                ResponseModel response = await serviceHelpers.GetRequestAsync(StringConstant.UsersEndPoint.Replace("{id}",id.ToString()), false, Utilities.AccessToken);
                if (response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<AppUser>(response.Data);
                }
                else
                {
                    var msg = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, msg.Detail, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return user;
        }

        public async Task<AppUser> GetAppUser()
        {
            AppUser user = null;
            try
            {
                ResponseModel response = await serviceHelpers.GetRequestAsync(StringConstant.AppUserEndPoint, true, Utilities.AccessToken);
                if (response.IsSuccess)
                {
                    user = JsonConvert.DeserializeObject<AppUser>(response.Data);
                }
                else
                {
                    var msg = JsonConvert.DeserializeObject<ErrorResponseModel>(response.Data);
                    await Application.Current.MainPage.DisplayAlert(DFResources.AlertText, msg.Detail, DFResources.OKText);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            return user;
        }
    }
}

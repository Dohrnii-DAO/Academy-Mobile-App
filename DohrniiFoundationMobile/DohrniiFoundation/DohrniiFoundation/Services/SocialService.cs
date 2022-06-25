using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.Socials;
using DohrniiFoundation.Resources;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DohrniiFoundation.Services
{
    public class SocialService : ISocialService
    {
        #region Private Members
        private static ServiceHelpers serviceHelpers;
        #endregion

        #region Constructor
        public SocialService()
        {
            serviceHelpers = ServiceHelpers.Instance;
        }
        #endregion

        public async Task<List<FriendRequest>> GetFriendRequests()
        {
            return null;
        }

        public async Task<List<AppUser>> GetFriends()
        {
            return null; ;
        }

        public async Task<FriendRequest> SendFriendRequests(int userId, FriendRequest friendRequest)
        {
            FriendRequest requestResp = null;
            try
            {
                string serializedRequest = JsonConvert.SerializeObject(friendRequest);
                ResponseModel response = await serviceHelpers.PostRequestAsync(serializedRequest, StringConstant.SendFriendRequestEndPoint.Replace("{user_id}", userId.ToString()), true, Utilities.AccessToken);
                
                if (response.IsSuccess)
                {
                    requestResp = JsonConvert.DeserializeObject<FriendRequest>(response.Data);
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

            return requestResp;
        }
    }
}

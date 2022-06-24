using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.Socials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DohrniiFoundation.Services
{
    public class SocialService : ISocialService
    {
        //private readonly IRestService _restClient;
        //public SocialService()
        //{
        //    _restClient = RefitHelper.GetService();
        //}

        public async Task<List<FriendRequest>> GetFriendRequests()
        {
            return null;
        }

        public async Task<List<AppUser>> GetFriends()
        {
            return null; ;
        }
    }
}

using DohrniiFoundation.Models.Socials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    public interface ISocialService
    {
        Task<List<AppUser>> GetFriends();
        Task<List<FriendRequest>> GetFriendRequests();
        Task<FriendRequest> SendFriendRequests(int userId, FriendRequest friendRequest);
    }
}

using DohrniiFoundation.Models.Socials;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    public interface ISocialService
    {
        Task<List<User>> GetFriends();
        Task<List<FriendRequest>> GetFriendRequests();
    }
}

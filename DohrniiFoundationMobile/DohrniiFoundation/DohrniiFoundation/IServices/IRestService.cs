using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.Socials;
using DohrniiFoundation.Models.UserModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    [Headers("User-Agent: Dohrnii Acedamy", "Accept: application/json")]
    public interface IRestService
    {
        [Post("/auth/token/")]
        Task<LoginResponse> Login([Body] LoginRequestModel loginRequest);

        [Get("/social/friends/")]
        Task<List<User>> GetFriends();
        
        [Get("/users/")]
        Task<List<User>> GetUsers();

        [Get("/users/{id}")]
        Task<User> GetUsers(int id);

        [Get("/social/friends/requests/")]
        Task<List<FriendRequest>> GetFriendRequests();

        [Post("//social/friends/request/{user_id}/")]
        Task<List<FriendRequest>> GetSendFriendRequests(string user_id);

    }
}

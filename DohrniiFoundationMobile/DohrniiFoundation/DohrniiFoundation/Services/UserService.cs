using DohrniiFoundation.Helpers;
using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.Socials;
using DohrniiFoundation.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.Services
{
    public class UserService : IUserService
    {
        private readonly IRestService _restClient;
        public UserService()
        {
            _restClient = RefitHelper.GetService();
        }

        public async Task<LoginResponse> Login(LoginRequestModel loginRequest)
        {
            var resp = await _restClient.Login(loginRequest);
            return resp;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _restClient.GetUsers();
        }

        public async Task<User> GetUsers(int id)
        {
            return await _restClient.GetUsers(id);
        }
    }
}

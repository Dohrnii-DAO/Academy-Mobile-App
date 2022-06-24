using DohrniiFoundation.Models.APIRequestModel;
using DohrniiFoundation.Models.Socials;
using DohrniiFoundation.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequestModel loginRequest);
        Task<List<AppUser>> GetUsers();
        Task<AppUser> GetUsers(int id);
    }
}

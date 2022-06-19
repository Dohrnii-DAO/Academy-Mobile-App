using DohrniiFoundation.Models.MockData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.IServices
{
    public interface IMockDataService
    {
        Task<List<LeaderBoard>> GetLeaderboard(string period);
        Task<List<Friend>> GetFriends();
    }
}

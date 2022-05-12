using DohrniiFoundation.IServices;
using DohrniiFoundation.Models.MockData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DohrniiFoundation.Services
{
    public class MockDataService : IMockDataService
    {
        public async Task<List<LeaderBoard>> GetLeaderboard(string period)
        {
            await Task.Delay(50);
            return new List<LeaderBoard>
            {
                new LeaderBoard
                {
                    Username = "@Limaco",
                    Profile = "defaultprofile.png",
                    XP = 300,
                    Badge = "goldBadge.png"
                },
                new LeaderBoard
                {
                    Username = "@Crypto.Kid",
                    Profile = "defaultprofile.png",
                    XP = 290,
                    Badge = "silverBadge.png"
                },
                new LeaderBoard
                {
                    Username = "@Wizard",
                    Profile = "defaultprofile.png",
                    XP = 150,
                    Badge = "bronzeBadge.png"
                }
            };
        }
    }
}

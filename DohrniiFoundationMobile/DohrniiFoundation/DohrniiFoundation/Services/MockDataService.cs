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
        public async Task<List<Friend>> GetFriends()
        {
            await Task.Delay(50);
            return new List<Friend>
            {
                new Friend
                {
                    Username = "@Limaco",
                    Profile = "defaultprofile.png",
                    XP = 300,
                    DHN = 2000,
                    Jelly = 30,
                    Score = 11
                },
                new Friend
                {
                    Username = "@Crypto.Kid",
                    Profile = "defaultprofile.png",
                    XP = 290,
                    DHN = 200,
                    Jelly = 45,
                    Score = -5
                },
                new Friend
                {
                    Username = "@Wizard",
                    Profile = "defaultprofile.png",
                    XP = 150,
                    DHN = 100,
                    Jelly = 50,
                    Score = -20
                },
                new Friend
                {
                    Username = "@Fabiolita",
                    Profile = "defaultprofile.png",
                    XP = 50,
                    DHN = 400,
                    Jelly = 10,
                    Score = 60
                },
                new Friend
                {
                    Username = "@Toochi",
                    Profile = "defaultprofile.png",
                    XP = 70,
                    DHN = 3000,
                    Jelly = 10,
                    Score = 30
                }
            };
        }

        public async Task<List<LeaderBoard>> GetLeaderboard(string period)
        {
            await Task.Delay(50);
            if(period == "Today")
            {
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
            else if (period == "Weekly")
            {
                return new List<LeaderBoard>
                {
                    new LeaderBoard
                    {
                        Username = "@Wizard",
                        Profile = "defaultprofile.png",
                        XP = 150,
                        Badge = "bronzeBadge.png"
                    },
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
                    }
                };
            }
            else
            {
                return new List<LeaderBoard>
                {
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
                    },
                    new LeaderBoard
                    {
                        Username = "@Limaco",
                        Profile = "defaultprofile.png",
                        XP = 300,
                        Badge = "goldBadge.png"
                    }
                };
            }
        }
    }
}

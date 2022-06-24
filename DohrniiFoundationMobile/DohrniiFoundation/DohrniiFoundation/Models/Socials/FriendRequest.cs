using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.Socials
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public AppUser Requester { get; set; }
        public AppUser Receiver { get; set; }
        public string Status { get; set; }
    }
}

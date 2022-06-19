using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.Socials
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public User Requester { get; set; }
        public User Receiver { get; set; }
        public string Status { get; set; }
    }
}

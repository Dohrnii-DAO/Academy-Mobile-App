using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace DohrniiFoundation.Models.Socials
{
    [AddINotifyPropertyChangedInterface]
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public bool IsPendingRequest { get; set; }
    }
}

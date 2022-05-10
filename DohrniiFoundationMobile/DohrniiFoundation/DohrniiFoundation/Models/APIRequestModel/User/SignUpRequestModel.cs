
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel
{
    /// <summary>
    /// This class is used to define the Signup API request properties
    /// </summary>
    public class SignUpRequestModel
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }
        [JsonProperty("lastname")]
        public string Lastname { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("access_type")]
        public string AccessType { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("phone")]
        public double? Phone { get; set; }
    }
}

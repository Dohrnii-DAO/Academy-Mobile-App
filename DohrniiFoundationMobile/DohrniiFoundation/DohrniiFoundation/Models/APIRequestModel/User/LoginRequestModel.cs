
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel
{
    /// <summary>
    /// This class is used to define the Login API request properties
    /// </summary>
    public class LoginRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("access_type")]
        public string AccessType { get; set; }
    }
}

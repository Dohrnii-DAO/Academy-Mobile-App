using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.User
{
    /// <summary>
    /// This class is used to define the update profile API request properties
    /// </summary>
    public class UpdateProfileRequestModel
    {
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }
    }
}

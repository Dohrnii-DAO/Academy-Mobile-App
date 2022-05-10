using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel
{
    /// <summary>
    /// Reset password API request of reset password screen
    /// </summary>
    public class ResetPasswordRequestModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

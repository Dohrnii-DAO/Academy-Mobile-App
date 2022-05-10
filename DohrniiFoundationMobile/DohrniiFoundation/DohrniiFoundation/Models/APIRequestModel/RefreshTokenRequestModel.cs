
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel
{
    /// <summary>
    /// Refresh token API request 
    /// </summary>
    public class RefreshTokenRequestModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}

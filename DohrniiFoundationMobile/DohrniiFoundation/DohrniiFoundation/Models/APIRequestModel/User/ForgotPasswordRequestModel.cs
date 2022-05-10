
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel
{
    /// <summary>
    /// This class is used to define the forgot password API request properties
    /// </summary>
    public class ForgotPasswordRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("access_type")]
        public string AccessType { get; set; }
    }
}

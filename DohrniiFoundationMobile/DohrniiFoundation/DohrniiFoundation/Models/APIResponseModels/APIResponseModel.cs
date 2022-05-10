
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels
{
    /// <summary>
    /// This class use to define class properties for API response
    /// </summary>
    public class APIResponseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("success")]        
        public bool Success { get; set; }
        [JsonProperty("expires_in")]
        public int Expires_in { get; set; }
        [JsonProperty("access_token")]
        public string Access_token { get; set; }
    }
}

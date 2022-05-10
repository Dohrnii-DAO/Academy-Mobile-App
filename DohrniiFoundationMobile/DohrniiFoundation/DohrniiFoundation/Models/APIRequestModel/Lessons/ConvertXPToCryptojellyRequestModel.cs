
using DohrniiFoundation.Helpers;
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This model class is to send the requested properties
    /// </summary>
    public class ConvertXPToCryptojellyRequestModel 
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("crptojelley")]
        public int Crptojelley { get; set; }
    }
}

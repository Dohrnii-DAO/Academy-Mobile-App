
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels
{
    /// <summary>
    /// login response class for all response from APIs
    /// </summary>
    public class ResponseModel : APIResponseModel
    {
        public bool IsSuccess { get; set; }
        [JsonProperty("data")]
        public string Data { get; set; }
        public int Code { get; set; }
    }
}

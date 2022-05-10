using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.User
{
    /// <summary>
    /// This model class is to get the response from login API
    /// </summary>
   public class LoginResponseModel : APIResponseModel
    {
        [JsonProperty("is_new_user")]
        public int IsNewUser { get; set; }
    }
}

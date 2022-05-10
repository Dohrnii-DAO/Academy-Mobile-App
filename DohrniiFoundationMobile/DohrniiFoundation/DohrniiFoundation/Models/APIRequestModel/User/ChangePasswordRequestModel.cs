using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.User
{
    /// <summary>
    /// This model class is used to send the request to change the password
    /// </summary>
   public class ChangePasswordRequestModel
    {
        [JsonProperty("old_password")]
        public string OldPassword { get; set; }
        [JsonProperty("new_password")]
        public string NewPassword { get; set; }
    }
}

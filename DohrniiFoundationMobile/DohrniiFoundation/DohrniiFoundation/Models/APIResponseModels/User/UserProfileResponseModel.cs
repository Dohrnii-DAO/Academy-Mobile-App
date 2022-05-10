using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace DohrniiFoundation.Models.APIResponseModels.User
{
    /// <summary>
    /// This model is used to get the response of the user
    /// </summary>
    public class UserProfileResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public UserDetails UserDetail { get; set; }
    }
    public class UserDetails
    {
        [JsonProperty("id")]
        public int UserId { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public long? Phone { get; set; }
        [JsonProperty("profile_image")]
        public string ProfileImage { get; set; }
        [JsonProperty("access_type")]
        public string AccessType { get; set; }
        [JsonProperty("is_verify")]
        public string IsVerify { get; set; }
        [JsonProperty("questionnaire_taken")]
        public int QuestionnaireTaken { get; set; }
        [JsonProperty("retake_required")]
        public int RetakeRequired { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}

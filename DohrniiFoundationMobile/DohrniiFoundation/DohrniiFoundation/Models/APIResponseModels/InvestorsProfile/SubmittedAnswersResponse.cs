using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIResponseModels.InvestorsProfile
{
    /// <summary>
    /// This class is used to define the submit selected answers API response properties of the questionnaire
    /// </summary>
    public class SubmittedAnswersResponse
    {
        #region Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("user_select_mapping")]
        public List<UserSelectMapping> UserSelectMapping { get; set; }
        [JsonProperty("retake_required")]
        public string RetakeRequired { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("deleted_at")]
        public object DeletedAt { get; set; }
        #endregion
    }
}

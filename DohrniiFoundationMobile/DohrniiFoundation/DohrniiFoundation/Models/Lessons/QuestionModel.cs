using DohrniiFoundation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is used to bind the questions of the class from API
    /// </summary>
    public class QuestionModel : ObservableObject
    {
        #region Public Properties
        [JsonProperty("id")]
        public int QuesId { get; set; }
        [JsonProperty("question_title")]
        public string QuestionTitle { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("xp_token")]
        public string XPToken { get; set; }
        [JsonProperty("answers")]
        public List<ClassAnswerModel> ClassAnswersList { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        public int QuestionNumber { get; set; }
        #endregion
    }
}

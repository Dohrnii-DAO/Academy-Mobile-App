using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This model is to send the request to submit the questions with answers of the class to API
    /// </summary>
    public class SubmitClassQuestionsRequestModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("chapter_id")]
        public string ChapterId { get; set; }
        [JsonProperty("lessons_id")]
        public string Lessonsid { get; set; }
        [JsonProperty("class_id")]
        public string ClassId { get; set; }
        [JsonProperty("collect_xp")]
        public string CollectXP { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("questionsAttempted")]
        public List<QuestionsAttempted> QuestionsAttempted { get; set; }
    }   
}

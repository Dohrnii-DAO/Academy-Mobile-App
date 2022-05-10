using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This model is to bind the submit chapters quiz in the App
    /// </summary>
    public class SubmitChapterQuiz
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("chapter_id")]
        public int ChapterId { get; set; }
        [JsonProperty("collect_DHN")]
        public string CollectDHN { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("deleted_at")]
        public object DeletedAt { get; set; }
    }
}

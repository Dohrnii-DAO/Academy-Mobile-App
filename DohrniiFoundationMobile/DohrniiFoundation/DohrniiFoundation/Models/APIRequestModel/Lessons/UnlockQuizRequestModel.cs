using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This request model class is to send the request to unlock the quiz
    /// </summary>
   public class UnlockQuizRequestModel 
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("chapter_id")]
        public int ChapterId { get; set; }
    }
}

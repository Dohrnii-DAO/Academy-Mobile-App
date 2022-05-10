using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This model class is to send the requested properties
    /// </summary>
    public class QuizAttempted
    {
        [JsonProperty("question_id")]
        public int QuestionId { get; set; }
        [JsonProperty("selected_answer_id")]
        public int SelectedAnswerId { get; set; }
        [JsonProperty("IsCorrect")]
        public int IsCorrect { get; set; }
    }
}

using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This model class is to bind the questions attempted for class questions
    /// </summary>
    public class QuestionsAttempted
    {
        [JsonProperty("question_id")]
        public string QuestionId { get; set; }
        [JsonProperty("selected_answer_id")]
        public string SelectedAnswerId { get; set; }
        [JsonProperty("IsCorrect")]
        public string IsCorrect { get; set; }
    }
}

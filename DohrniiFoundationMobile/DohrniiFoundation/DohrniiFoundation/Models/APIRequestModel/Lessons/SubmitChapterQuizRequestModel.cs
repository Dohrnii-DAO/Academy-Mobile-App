
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIRequestModel.Lessons
{
    /// <summary>
    /// This model class is to send the requested properties
    /// </summary>
    public class SubmitChapterQuizRequestModel
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("chapter_id")]
        public int ChapterId { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("correct_question_percentage")]
        public int CorrectQuestionPercentage { get; set; }
        [JsonProperty("questionsAttempted")]
        public List<QuizAttempted> QuestionsAttempted { get; set; }
    }    
}

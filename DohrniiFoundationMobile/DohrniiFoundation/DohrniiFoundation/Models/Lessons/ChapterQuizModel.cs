using DohrniiFoundation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.Lessons
{
    /// <summary>
    /// This model class is to bind the chapters quiz
    /// </summary>
   public class ChapterQuizModel : ObservableObject
    {
        #region Public Properties
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("question_title")]
        public string QuestionTitle { get; set; }
        [JsonProperty("answers")]
        public List<QuizAnswerModel> QuizAnswers { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        #endregion
    }   
}

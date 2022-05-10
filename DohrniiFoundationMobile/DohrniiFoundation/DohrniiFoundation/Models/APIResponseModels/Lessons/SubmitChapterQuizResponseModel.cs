
using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This response model class is used to get the data from API
    /// </summary>
    public class SubmitChapterQuizResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public SubmitChapterQuiz SubmitChapterQuizData { get; set; }
    }   
}

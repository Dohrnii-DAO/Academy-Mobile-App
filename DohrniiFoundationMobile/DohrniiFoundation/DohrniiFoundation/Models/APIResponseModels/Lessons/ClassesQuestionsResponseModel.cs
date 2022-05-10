using DohrniiFoundation.Models.Lessons;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This response model is to bind the response of the classes questions from API
    /// </summary>
   public class ClassesQuestionsResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public ObservableCollection<QuestionModel> ClassQuestions { get; set; }
    }
}

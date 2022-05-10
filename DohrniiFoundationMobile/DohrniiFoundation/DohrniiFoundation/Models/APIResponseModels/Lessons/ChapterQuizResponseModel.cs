
using DohrniiFoundation.Models.Lessons;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
  public class ChapterQuizResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public ObservableCollection<ChapterQuizModel> QuizQuestions { get; set; }
    }
}

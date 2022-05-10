using DohrniiFoundation.Models.Lessons;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This response model is to get the data from the API
    /// </summary>
   public class LessonsResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public ObservableCollection<ChaptersModel> AllChapters { get; set; }
    }
}

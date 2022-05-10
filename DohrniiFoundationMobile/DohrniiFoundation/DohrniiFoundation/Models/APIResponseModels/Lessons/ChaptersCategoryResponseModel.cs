using DohrniiFoundation.Models.Lessons;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This model class is to bind the chapters categories response of the API
    /// </summary>
   public class ChaptersCategoryResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public ObservableCollection<ChaptersCategoryModel> Categories { get; set; }
    }
}

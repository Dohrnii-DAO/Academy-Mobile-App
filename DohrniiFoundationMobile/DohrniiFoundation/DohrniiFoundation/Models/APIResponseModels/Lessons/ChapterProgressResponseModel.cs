using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This class is used to define the chapters progress API response properties
    /// </summary>
    public class ChapterProgressResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public ProgressModel ProgressData { get; set; }
    }   
}

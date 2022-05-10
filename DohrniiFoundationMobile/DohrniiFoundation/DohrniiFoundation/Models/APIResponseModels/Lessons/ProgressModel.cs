using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This model class is to get the response of the progress of the chapter from API
    /// </summary>
    public class ProgressModel
    {        
        [JsonProperty("total_lesson")]
        public int TotalLesson { get; set; }
        [JsonProperty("complete_lesson")]
        public int CompleteLesson { get; set; }
        [JsonProperty("total_class")]
        public int TotalClass { get; set; }
        [JsonProperty("complete_class")]
        public int CompleteClass { get; set; }
    }
}

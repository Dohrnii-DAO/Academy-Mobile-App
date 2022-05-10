using Newtonsoft.Json;

namespace DohrniiFoundation.Models.APIResponseModels.User
{
    /// <summary>
    /// This class is to bind the response of the lessons progress in the app
    /// </summary>
    public class LessonInprogress
    {
        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
        [JsonProperty("lesson_name")]
        public string LessonName { get; set; }
    }
}

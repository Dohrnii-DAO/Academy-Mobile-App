using DohrniiFoundation.Models.Lessons;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIResponseModels.Lessons
{
    /// <summary>
    /// This response model class is to bind the response of the classes
    /// </summary>
    public class ClassesResponseModel : APIResponseModel
    {
        [JsonProperty("data")]
        public List<ClassModel> AllClasses { get; set; }
    }
}

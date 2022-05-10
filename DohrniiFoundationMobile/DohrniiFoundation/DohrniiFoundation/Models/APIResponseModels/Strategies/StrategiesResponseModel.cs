using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels
{
    /// <summary>
    /// This class is used to define the strategies API response properties
    /// </summary>
    public class StrategiesResponseModel : APIResponseModel
    {
        #region Properties
        //[JsonProperty("data")]
        //public ObservableCollection<AllStrategiesModel> AllStrategies { get; set; }
        [JsonProperty("total")]
        public int TotalStrategies { get; set; }
        [JsonProperty("page")]
        public string Page { get; set; }
        #endregion
    }
}

using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.APIResponseModels.Strategies
{
    /// <summary>
    /// This class is used to define the long term concepts API response properties
    /// </summary>
    public class LongTermConceptsResponseModel : APIResponseModel
    {
        #region Properties
        //[JsonProperty("data")]
        //public ObservableCollection<LongTermInvestmentConcept> LongTermInvestmentConcepts { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("page")]
        public string Page { get; set; }
        #endregion
    }
}

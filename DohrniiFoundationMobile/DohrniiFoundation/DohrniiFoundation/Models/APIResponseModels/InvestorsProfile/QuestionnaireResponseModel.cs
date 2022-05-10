using DohrniiFoundation.Models.APIResponseModels;
using DohrniiFoundation.Models.InvestorsProfileModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DohrniiFoundation.Models.InvestorsProfileResponseModels
{
    /// <summary>
    /// This class is used to define the questions API response properties
    /// </summary>
    public class QuestionnaireResponseModel : APIResponseModel
    {
        #region Properties
        [JsonProperty("data")]
        public ObservableCollection<QuestionnaireModel> Questions { get; set; }
        #endregion
    }
}

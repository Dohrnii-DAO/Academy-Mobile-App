using Newtonsoft.Json;
using System.Collections.Generic;

namespace DohrniiFoundation.Models.APIResponseModels.User
{
    /// <summary>
    /// This class is to bind the resposne of the user status from the API
    /// </summary>
    public class UserStatusDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("assigned_strategy_id")]
        public int AssignedStrategyId { get; set; }
        [JsonProperty("total_xp")]
        public int TotalXP { get; set; }
        [JsonProperty("total_cryptoJelly")]
        public int TotalCryptoJelly { get; set; }
        [JsonProperty("total_dhn")]
        public decimal TotalDHN { get; set; }
        [JsonProperty("xp_per_crypto_jelly")]
        public string XPPerCryptoJelly { get; set; }
        [JsonProperty("lesson_inprogress")]
        public List<LessonInprogress> LessonInProgress { get; set; }
    }
}

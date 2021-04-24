using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZaxHerbivoryTrainer.APP.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserModel
    {
        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "HashUser")]
        public Guid? HashUser { get; set; }
        [JsonProperty(PropertyName = "CreatedUtc")]
        public DateTime CreatedUtc { get; set; }
        [JsonProperty(PropertyName = "FinishedPhase1Utc")]
        public DateTime? FinishedPhase1Utc { get; set; }
        [JsonProperty(PropertyName = "FinishedPhase2Utc")]
        public DateTime? FinishedPhase2Utc { get; set; }
        [JsonProperty(PropertyName = "FinishedPhase3Utc")]
        public DateTime? FinishedPhase3Utc { get; set; }
        [JsonProperty(PropertyName = "TimePhase1")]
        public DateTime? TimePhase1 { get; set; }
        [JsonProperty(PropertyName = "TimePhase2")]
        public DateTime? TimePhase2 { get; set; }
        [JsonProperty(PropertyName = "TimePhase3")]
        public DateTime? TimePhase3 { get; set; }
        [JsonProperty(PropertyName = "PictureCycledPhase1")]
        public int? PictureCycledPhase1 { get; set; }
        [JsonProperty(PropertyName = "PictureCycledPhase2")]
        public int? PictureCycledPhase2 { get; set; }
        [JsonProperty(PropertyName = "PictureCycledPhase3")]
        public int? PictureCycledPhase3 { get; set; }
        [JsonProperty(PropertyName = "FinishingPercentPhase1")]
        public decimal? FinishingPercentPhase1 { get; set; }
        [JsonProperty(PropertyName = "FinishingPercentPhase2")]
        public decimal? FinishingPercentPhase2 { get; set; }
        [JsonProperty(PropertyName = "FinishingPercentPhase3")]
        public decimal? FinishingPercentPhase3 { get; set; }


        [JsonProperty(PropertyName = "Guesses")]
        public virtual ICollection<UserGuessModel> Guesses { get; set; }
    }
}

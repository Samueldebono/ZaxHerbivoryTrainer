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
        [JsonProperty(PropertyName = "FinishedUtc")]
        public DateTime? FinishedUtc { get; set; }
        [JsonProperty(PropertyName = "Time")]
        public DateTime? Time { get; set; }
        [JsonProperty(PropertyName = "PictureCycled")]
        public int? PictureCycled { get; set; }
        [JsonProperty(PropertyName = "FinishingPercent")]
        public decimal? FinishingPercent { get; set; }

        [JsonProperty(PropertyName = "Guesses")]
        public virtual ICollection<UserGuessModel> Guesses { get; set; }
    }
}

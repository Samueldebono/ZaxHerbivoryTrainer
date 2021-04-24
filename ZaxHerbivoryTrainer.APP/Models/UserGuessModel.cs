using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZaxHerbivoryTrainer.APP.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class UserGuessModel
    {
        [JsonProperty(PropertyName = "UsersGuessId")]
        public int UsersGuessId { get; set; }

        [JsonProperty(PropertyName = "GuessPercentage")]
        public decimal GuessPercentage { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "ImageId")]
        public int ImageId { get; set; }
        [JsonProperty(PropertyName = "Phase")]
        public Phase Phase { set; get; }
        [JsonProperty(PropertyName = "Image")]
        public virtual ImageModel Image { set; get; }

        //public virtual User User { set; get; }
    }

    public enum Phase:byte
    {
        ONE =0,
        TWO= 1,
        THREE =2
    }
}

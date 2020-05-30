using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZaxHerbivoryTrainer.APP.Bindings
{
    public class UpdateUserBinding
    {
        [JsonProperty("FinishedUtc")]
        public DateTime? FinishedUtc { get; set; }
        [JsonProperty("FinishingPercent")]
        public decimal FinishingPercent { get; set; }
        [JsonProperty("PictureCycled")]
        public int PictureCycled { get; set; }
        [JsonProperty("Time")]
        public DateTime Time { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.API.Bindings
{
    [DataContract]
    public class UpdateUserBinding
    {
        [DataMember(Name = "FinishedUtc")]
        public DateTime? FinishedUtc { get; set; }
        [DataMember(Name = "FinishingPercent")]
        public decimal FinishingPercent { get; set; }
        [DataMember(Name = "PictureCycled")]
        public int PictureCycled { get; set; }
        [DataMember(Name = "Time")]
        public DateTime Time { get; set; }
    }
}

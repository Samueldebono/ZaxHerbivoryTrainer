using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZaxHerbivoryTrainer.APP.Models
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageModel
    {
        [JsonProperty(PropertyName = "ImageId")]
        public int ImageId { get; set; }
        //[JsonProperty(PropertyName = "CloudinaryId")]
        //public Guid CloudinaryId { get; set; }
        //[JsonProperty(PropertyName = "AddedUtc")]
        //public DateTime AddedUtc { get; set; }
        [JsonProperty(PropertyName = "FileName")]
        public string FileName { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "DecayRate")]
        public decimal DecayRate { get; set; }
        //[JsonProperty(PropertyName = "Delete")]
        //public bool Delete { get; set; }
        //[JsonProperty(PropertyName = "DeletedUtc")]
        //public DateTime DeletedUtc { get; set; }
        //public string CloudinaryUrl { get; set; }
    }
}

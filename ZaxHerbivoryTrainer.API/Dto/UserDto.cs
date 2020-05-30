using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;

namespace ZaxHerbivoryTrainer.API.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember(Name = "UserId")]
        public int UserId { get; set; }
        [DataMember(Name = "HashUser")]
        public Guid? HashUser { get; set; }
        [DataMember(Name = "CreatedUtc")]
        public DateTime CreatedUtc { get; set; }
        [DataMember(Name = "FinishedUtc")]
        public DateTime? FinishedUtc { get; set; }
        [DataMember(Name = "Time")]
        public DateTime? Time { get; set; }
        [DataMember(Name = "PictureCycled")]
        public int? PictureCycled { get; set; }
        [DataMember(Name = "FinishingPercent")]
        public decimal? FinishingPercent { get; set; }
        [DataMember(Name = "Guesses")]
        public virtual ICollection<UsersGuessDto> Guesses { get; set; }
    }
}

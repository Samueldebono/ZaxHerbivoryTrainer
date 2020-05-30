using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ZaxHerbivoryTrainer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public Guid? HashUser { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? FinishedUtc { get; set; }
        public DateTime? Time { get; set; }
        public int? PictureCycled { get; set; }
        public decimal? FinishingPercent { get; set; }

        public virtual ICollection<UsersGuess> Guesses { get; set; }
    }
}

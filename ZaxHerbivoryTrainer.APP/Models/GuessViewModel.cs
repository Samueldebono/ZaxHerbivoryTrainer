using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class GuessViewModel
    {
        public Guid UserHash { get; set; }
        public string CurrentCloudinaryUrl { get; set; }
        public int UserId { get; set; }
        public int CurrentImageId { get; set; }
        public decimal? GuessResultPercent { get; set; }
        public int Timer { get; set; }
        public int? ImagesUsed { get; set; }
        public byte Phase { get; set; }
        public decimal? FinalPercentage  { get; set; }
        public bool ReturningUser  { get; set; }
        public int ReturningTimer  { get; set; }

    }
    public enum GuessResult : byte
    {
        NotAnswered = 0,
        Right = 1,
        Wrong = 2
    }
}

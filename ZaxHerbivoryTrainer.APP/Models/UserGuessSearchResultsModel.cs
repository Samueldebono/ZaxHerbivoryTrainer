using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class UserGuessSearchResultsModel
    {
        public int UserId { get; set; }

        public string HashUser { get; set; }

        public string CreatedUtc { get; set; }

        public string FinishedUtc { get; set; }

        public string Time { get; set; }
        public string PictureCycled { get; set; }
        public string FinishingPercent { get; set; }
    }
}

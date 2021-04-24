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
        public string RetentionUser { get; set; }
        public string FinishedPhase1Utc { get; set; }
        public string FinishedPhase2Utc { get; set; }
        public string FinishedPhase3Utc { get; set; }
        public string TimePhase1 { get; set; }
        public string TimePhase2 { get; set; }
        public string TimePhase3 { get; set; }
        public string PictureCycledPhase1 { get; set; }
        public string PictureCycledPhase2 { get; set; }
        public string PictureCycledPhase3 { get; set; }
        public string FinishingPercentPhase1 { get; set; }
        public string FinishingPercentPhase2 { get; set; }
        public string FinishingPercentPhase3 { get; set; }

        //public string HashUser { get; set; }

        //public string CreatedUtc { get; set; }

        //public string FinishedUtc { get; set; }

        //public string Time { get; set; }
        //public string PictureCycled { get; set; }
        //public string FinishingPercent { get; set; }
    }
}

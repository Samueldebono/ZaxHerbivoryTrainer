using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class FinishedModelView
    {
        public int[] NumberOfImages { get; set; }
        public decimal[] GuessResult { get; set; }

        public string TotalTime { get; set; }
        public string AverageTime { get; set; }
        public string UserGuid { get; set; }
    }
}

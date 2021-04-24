using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class CompletedModelView
    {
        public int[] NumberOfImagesPhase1 { get; set; }
        public int[] NumberOfImagesPhase2 { get; set; }
        public int[] NumberOfImagesPhase3 { get; set; }
        public decimal[] GuessResultPhase1 { get; set; }
        public decimal[] GuessResultPhase2 { get; set; }
        public decimal[] GuessResultPhase3 { get; set; }

        public string TotalTimePhase1 { get; set; }
        public string TotalTimePhase2 { get; set; }
        public string TotalTimePhase3 { get; set; }
        public string AverageTimePhase1 { get; set; }
        public string AverageTimePhase2 { get; set; }
        public string AverageTimePhase3 { get; set; }
        public string UserGuid { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}

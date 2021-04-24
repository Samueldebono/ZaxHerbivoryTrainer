using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class UserCompletionDataSearchResultsModel
    {
        public string UserId { get; set; }

        public List<double> GuessResult { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class UserCompletionDataSearchModel
    {
        public List<UserCompletionDataSearchResultsModel> UserCompletionDataSearchResults { get; set; }
        public int MaxGuess { get; set; }
    }
}

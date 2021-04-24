using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class UserEmailSearchResultsModel
    {
        public int UserEmailId { get; set; }
        public string CreatedUtc { get; set; }
        public string Email { get; set; }
        public bool PrizeSent { get; set; }
    }
}

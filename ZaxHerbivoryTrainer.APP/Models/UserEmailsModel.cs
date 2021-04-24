using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.API.Models
{
    public class UserEmailsModel
    {
        public int UserEmailId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string Email { get; set; }
        public bool PrizeSent { get; set; }

    }
}

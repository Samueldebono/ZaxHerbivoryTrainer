using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.API.Bindings
{
    public class AuthBinding
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public byte RoleType { get; set; }
    }
}

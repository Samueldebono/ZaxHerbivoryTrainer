using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.API.Bindings
{
    public class SearchImageBinding
    {
        public int[] PreviousImageIds { get; set; }
        public bool? ReturnRandom { get; set; }
    }
}

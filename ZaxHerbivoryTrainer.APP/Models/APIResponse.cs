using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ZaxHerbivoryTrainer.APP.Models
{
    public class APIResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T Content { get; set; }
        public string Error { get; set; }
        public string Errors { get; set; }
    }
}

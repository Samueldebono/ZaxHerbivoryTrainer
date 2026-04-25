using ZaxHerbivoryTrainer.API.Bindings;
using ZaxHerbivoryTrainer.API.Services;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace ZaxHerbivoryTrainer.API.Controllers
{
    [Route("api")]
    [ApiController]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 1)]
    [Produces("application/json")]
    public class VisitCounterController : ControllerBase
    {

        private readonly IZaxHerbivoryTrainerRepository _ZaxHerbivoryTrainerRepository;
        
        public VisitCounterController(IZaxHerbivoryTrainerRepository ZaxHerbivoryTrainerRepository, IMapper mapper)
        {
            _ZaxHerbivoryTrainerRepository =
                ZaxHerbivoryTrainerRepository ?? throw new ArgumentNullException(nameof(ZaxHerbivoryTrainerRepository));
        }

        [AllowAnonymous]
        [HttpPost("visitCounter", Name = "GetVisitCounter")]
        public ActionResult<int> GetVisitCounter()
        { 
            var visitCounter =  _ZaxHerbivoryTrainerRepository.GetUpdateVisitCount();
            return Ok(visitCounter.Count);
        }



    }
}

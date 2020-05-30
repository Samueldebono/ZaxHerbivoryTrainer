using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;
using AutoMapper;
using ZaxHerbivoryTrainer.API.Bindings;
using ZaxHerbivoryTrainer.API.Services;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ZaxHerbivoryTrainer.API.Controllers
{
    [Route("api")]
    [ApiController]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 10)]
    [HttpCacheValidation(MustRevalidate = true)]
    [Produces("application/json")]
    public class UserGuessController : ControllerBase
    {

        private readonly IZaxHerbivoryTrainerRepository _ZaxHerbivoryTrainerRepository;
        private readonly IMapper _mapper;

        public UserGuessController(IZaxHerbivoryTrainerRepository ZaxHerbivoryTrainerRepository, IMapper mapper)
        {
            _ZaxHerbivoryTrainerRepository =
                ZaxHerbivoryTrainerRepository ?? throw new ArgumentNullException(nameof(ZaxHerbivoryTrainerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet("usersGuess", Name = "GetUsersGuess")]
        public ActionResult<List<UsersGuess>> GetUserGuess()
        {
            var guesses = _ZaxHerbivoryTrainerRepository.GetUserGuesses();

            if (guesses == null || !guesses.Any())
                return NotFound();
            var result = _mapper.Map<List<UsersGuess>>(guesses);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("usersGuess", Name = "CreateUsersGuess")]
        public IActionResult CreateUserGuess([FromBody]UserGuessBinding binding)
        {

            var userGuess = new UsersGuess
            {
                GuessPercentage = binding.GuessPercentage,
                ImageId = binding.ImageId,
                UserId = binding.UserId
            };

            var newUserGuess = _ZaxHerbivoryTrainerRepository.CreateUserGuess(userGuess);
            _ZaxHerbivoryTrainerRepository.Save();

            return Ok(newUserGuess);
        }
        [Authorize]
        [HttpGet("usersGuess/{id}")]
        public IActionResult GetUserGuess(int id)
        {

            var userGuesses = _ZaxHerbivoryTrainerRepository.GetUserGuesses();
            var result = userGuesses.Where(x => x.UserId == id);

            return Ok(result);
        }
    }
}
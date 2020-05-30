using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Entities;
using API.ZaxHerbivoryTrainer.Models;
using AutoMapper;
using EasyPT.API.ActionConstraints;
using ZaxHerbivoryTrainer.API.Dto;
using ZaxHerbivoryTrainer.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using System.Text.Json;
using ZaxHerbivoryTrainer.API.Bindings;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ZaxHerbivoryTrainer.API.Controllers
{
    [Route("api")]
    [ApiController]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 0)]
    [HttpCacheValidation(MustRevalidate = true)]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IZaxHerbivoryTrainerRepository _ZaxHerbivoryTrainerRepository;
        private readonly IMapper _mapper;



        public UserController(IZaxHerbivoryTrainerRepository ZaxHerbivoryTrainerRepository, IMapper mapper)
        {
            _ZaxHerbivoryTrainerRepository = ZaxHerbivoryTrainerRepository ?? throw new ArgumentNullException(nameof(ZaxHerbivoryTrainerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet("users", Name = "GetUsers")]
        public  ActionResult<List<UserDto>> GetUsers()
        {
            var user = _ZaxHerbivoryTrainerRepository.GetUsers();
            if (user == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<List<UserDto>>(user);
            return Ok(results);
        }

        [HttpGet("user/hash/{id}", Name = "GetUserHash")]
        [Authorize]
        public  ActionResult<UserDto> GetUsers(Guid id)
        {
            var user = _ZaxHerbivoryTrainerRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }

        [Authorize]
        [HttpGet("user/{id}", Name = "GetUser")]
        public  ActionResult<UserDto> GetUsers(int id)
        {
            var user = _ZaxHerbivoryTrainerRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }

        [Authorize]
        [HttpPost("user")]
        public ActionResult<UserDto> CreateUser()
        {
            var user = new User()
            {
                CreatedUtc = DateTime.UtcNow,
                HashUser = Guid.NewGuid()
            };
            _ZaxHerbivoryTrainerRepository.CreateUser(user);
            _ZaxHerbivoryTrainerRepository.Save();

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }

        [Authorize]
        [HttpPost("user/hash/{id}")]
        public ActionResult<UserDto> UpdateUser(Guid id, [FromBody] UpdateUserBinding binding)
        {
            var user = _ZaxHerbivoryTrainerRepository.GetUser(id);

            if (user == null)
                return NotFound();
            user.FinishedUtc = binding.FinishedUtc;
            user.Time = binding.Time;
            user.FinishingPercent = binding.FinishingPercent;
            user.PictureCycled = binding.PictureCycled;

            _ZaxHerbivoryTrainerRepository.Save();
            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }

        [Authorize]
        [HttpPost("user/{id}", Name = "UpdateUser")]
        public IActionResult UpdateUserViaId(int id, [FromBody] UpdateUserBinding binding)
        {

            var user = _ZaxHerbivoryTrainerRepository.GetUser(id);

            if (user == null)
                return NotFound();
            user.FinishedUtc = binding.FinishedUtc;
            user.Time = binding.Time;
            user.FinishingPercent = binding.FinishingPercent;
            user.PictureCycled = binding.PictureCycled;

            _ZaxHerbivoryTrainerRepository.Save();

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }
    }
}
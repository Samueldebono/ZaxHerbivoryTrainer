﻿using System;
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
using ZaxHerbivoryTrainer.API.Models;
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

            switch (binding.Phase)
            {
                case 0:
                    user.FinishedPhase1Utc = binding.FinishedUtc;
                    user.TimePhase1 = binding.Time;
                    user.FinishingPercentPhase1 = binding.FinishingPercent;
                    user.PictureCycledPhase1 = binding.PictureCycled;
                    break;
                case 1:
                    user.FinishedPhase2Utc = binding.FinishedUtc;
                    user.TimePhase2 = binding.Time;
                    user.FinishingPercentPhase2 = binding.FinishingPercent;
                    user.PictureCycledPhase2 = binding.PictureCycled;
                    break;
                case 2:
                    user.FinishedPhase3Utc = binding.FinishedUtc;
                    user.TimePhase3 = binding.Time;
                    user.FinishingPercentPhase3 = binding.FinishingPercent;
                    user.PictureCycledPhase3 = binding.PictureCycled;
                    break;
            }

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
            switch (binding.Phase)
            {
                case 0:
                    user.FinishedPhase1Utc = binding.FinishedUtc;
                    user.TimePhase1 = binding.Time;
                    user.FinishingPercentPhase1 = binding.FinishingPercent;
                    user.PictureCycledPhase1 = binding.PictureCycled;
                    break;
                case 1:
                    user.FinishedPhase2Utc = binding.FinishedUtc;
                    user.TimePhase2 = binding.Time;
                    user.FinishingPercentPhase2 = binding.FinishingPercent;
                    user.PictureCycledPhase2 = binding.PictureCycled;
                    break;
                case 2:
                    user.FinishedPhase3Utc = binding.FinishedUtc;
                    user.TimePhase3 = binding.Time;
                    user.FinishingPercentPhase3 = binding.FinishingPercent;
                    user.PictureCycledPhase3 = binding.PictureCycled;
                    break;
            }

            _ZaxHerbivoryTrainerRepository.Save();

            var results = _mapper.Map<UserDto>(user);
            return Ok(results);
        }

        [Authorize]
        [HttpGet("users/emails", Name = "GetUserEmails")]
        public IActionResult GetUserEmails()
        {

            var userEmails = _ZaxHerbivoryTrainerRepository.GetUserEmails();

            if (userEmails == null)
                return NotFound();
         

            var results = _mapper.Map<List<UserEmailDto>>(userEmails);
            return Ok(results);
        }

        [Authorize]
        [HttpPost("users/email", Name = "CreateUserEmails")]
        public IActionResult CreateUserEmails([FromBody] CreateUserEmailBinding binding)
        {
            var email = _mapper.Map<UserEmails>(binding);
            var userEmails = _ZaxHerbivoryTrainerRepository.CreateUserEmail(email);
            _ZaxHerbivoryTrainerRepository.Save();
            var results = _mapper.Map<UserEmailDto>(userEmails);

            return Ok(results);
        }
    }
}
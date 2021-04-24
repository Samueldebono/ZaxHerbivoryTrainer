using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;
using AutoMapper;
using ZaxHerbivoryTrainer.API.Bindings;
using ZaxHerbivoryTrainer.API.Dto;
using ZaxHerbivoryTrainer.API.Models;

namespace ZaxHerbivoryTrainer.API.Profiles
{
    public class UserEmailProfile : Profile
    {
        public UserEmailProfile()
        {
            CreateMap<UserEmails, UserEmailDto>();
            CreateMap<CreateUserEmailBinding, UserEmails>();
        }
    }
}

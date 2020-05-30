using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;
using AutoMapper;
using ZaxHerbivoryTrainer.API.Dto;

namespace ZaxHerbivoryTrainer.API.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDto>();
        }
    }
}

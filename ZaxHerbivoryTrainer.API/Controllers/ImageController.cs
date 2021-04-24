using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ZaxHerbivoryTrainer.Models;
using AutoMapper;
using ZaxHerbivoryTrainer.API.Bindings;
using ZaxHerbivoryTrainer.API.Dto;
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
    public class ImageController: ControllerBase
    {
        private readonly IZaxHerbivoryTrainerRepository _ZaxHerbivoryTrainerRepository;
        private readonly IMapper _mapper;

        public ImageController(IZaxHerbivoryTrainerRepository ZaxHerbivoryTrainerRepository, IMapper mapper)
        {
            _ZaxHerbivoryTrainerRepository = ZaxHerbivoryTrainerRepository ?? throw new ArgumentNullException(nameof(ZaxHerbivoryTrainerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize]
        [HttpGet("image/{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            var image = _ZaxHerbivoryTrainerRepository.GetImage(id);
            if (image == null)
                return NotFound();
            return Ok(image);
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet("images")]
        public ActionResult<List<ImageDto>> GetAllImages()
        {
            var images = _ZaxHerbivoryTrainerRepository.GetImages();
            if (images == null)
                return NotFound();
            var results = _mapper.Map<List<ImageDto>>(images);
            return Ok(results);
        }

        [Authorize]
        [HttpGet("image")]
        public ActionResult<List<ImageDto>> GetRandomImage(SearchImageBinding binding)
        {
            var images = _ZaxHerbivoryTrainerRepository.GetImages();
            if (images == null)
                return NotFound();
            if (binding.PreviousImageIds != null && binding.PreviousImageIds.Length > 0)
                images = images.Where(x => !binding.PreviousImageIds.Contains(x.ImageId));
            if (binding.ReturnRandom.HasValue && binding.ReturnRandom.Value)
            {
                Random rnd = new Random();
                images = images.OrderBy(x => rnd.Next()).ToList();
            }
            var results = _mapper.Map<ImageDto>(images.FirstOrDefault());
            return Ok(results);
        }

    }
}

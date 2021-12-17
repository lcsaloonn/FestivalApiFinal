using System;
using System.Collections.Generic;
using Application.UserCase.Hoodie;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Hoodies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Hoodie")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HoodieController:ControllerBase
    {
        private readonly IHoodiesRepository _repository;
        private readonly IMapper _mapper;

        public HoodieController(IHoodiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/Hoodie
        [HttpGet]
        public ActionResult<IEnumerable<HoodieReadDto>> GetAllHoodies()
        {
            var hoodieItems = _repository.GetAllHoodies();

            return Ok(_mapper.Map<IEnumerable<HoodieReadDto>>(hoodieItems)); // 200 success
        }
        //GET api/Hoodie/{id}
        
        [HttpGet("{id}", Name = "GetHoodieById")]
        public ActionResult<HoodieReadDto> GetHoodieById(Guid id)
        {
            var hoodieItems = _repository.GetHoodieById(id);
            if (hoodieItems != null)
            {
                return Ok(_mapper.Map<HoodieReadDto>(hoodieItems));
            }

            return NotFound();

        }
        
        //POST api/Hoodie
        [HttpPost]
        public ActionResult<HoodieReadDto> CreateHoodie(HoodieCreateDto hoodieCreateDto)
        {
            var hoodieModel = _mapper.Map<Hoodies>(hoodieCreateDto);
            _repository.CreateHoodie(hoodieModel);
            _repository.SaveChanges();

            var hoodieReadDto = _mapper.Map<HoodieReadDto>(hoodieModel);
            //return Ok(commandModel);
            return CreatedAtRoute(nameof(GetHoodieById), new {Id = hoodieReadDto.Id}, hoodieReadDto); 
        }
            
        //PUT api/Hoodies/{id}
        //used to update everything
        [HttpPut("{id}")]
        public ActionResult UpdateHoodie(Guid id, HoodieUpdateDto hoodieUpdateDto)
        {
            var hoodieModelFromRepo = _repository.GetHoodieById(id);
            if (hoodieModelFromRepo == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(hoodieUpdateDto, hoodieModelFromRepo); 
            _repository.UpdateHoodie(hoodieModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        
        //PATCH  api/hoodie/{id}
        //ToDO verification du patch 
        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(Guid id, JsonPatchDocument<HoodieUpdateDto> patchDocument)
        {
            //check we have a resource to update from our repository
            var hoodieModelFromRepo = _repository.GetHoodieById(id);
            if (hoodieModelFromRepo == null)
            {
                return new NotFoundResult();
            }

            // generate a new entity of a commandDTO
            var hoodieToPatch = _mapper.Map<HoodieUpdateDto>(hoodieModelFromRepo);
            //apply the patch
            patchDocument.ApplyTo(hoodieToPatch, ModelState);
            if (!TryValidateModel(hoodieToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(hoodieToPatch, hoodieModelFromRepo);
            _repository.UpdateHoodie(hoodieModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        //DELETE api/hoodie/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteHoodie(Guid id)
        {
            var hoodieModelFromRepo = _repository.GetHoodieById(id);
            if (hoodieModelFromRepo == null)
            {
                return new NotFoundResult();
            }
            _repository.DeleteHoodie(hoodieModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }


    }
}
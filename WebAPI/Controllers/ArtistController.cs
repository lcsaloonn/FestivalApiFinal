using System;
using System.Collections.Generic;
using Application.UserCase.Artist;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Artiste;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Artist")]
    [ApiController]
    public class ArtistController:ControllerBase
    {
        private readonly IArtistRepository _repository;
        private readonly IMapper _mapper;

        public ArtistController(IArtistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/Artist
        [HttpGet]
        public ActionResult<IEnumerable<ArtistReadDto>> GetAllArtist()
        {
            var artistItems = _repository.GetAllArtistes();

            return Ok(_mapper.Map<IEnumerable<ArtistReadDto>>(artistItems)); // 200 success
        }
        
        //GET api/Artist/{id}
        [HttpGet("{id}", Name = "GetArtistById")]
        public ActionResult<ArtistReadDto> GetArtistById(Guid id)
        {
            var artistItems = _repository.GetArtisteById(id);
            if (artistItems != null)
            {
                return Ok(_mapper.Map<ArtistReadDto>(artistItems));
            }

            return NotFound();

        }
        //POST api/Artist
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ArtistReadDto> CreateArtist(ArtistCreateDto artistCreateDto)
        {
            var artistModel = _mapper.Map<Artiste>(artistCreateDto);
            _repository.CreateArtiste(artistModel);
            _repository.SaveChanges();

            var artistReadDto = _mapper.Map<ArtistReadDto>(artistModel);
            //return Ok(commandModel);
            return CreatedAtRoute(nameof(GetArtistById), new {Id = artistReadDto.Id}, artistReadDto); 
        }
        
        //PUT api/Artist/{id}
        //used to update everything
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult UpdateArtist(Guid id, ArtistUpdateDto artistUpdateDto)
        {
            var artistModelFromRepo = _repository.GetArtisteById(id);
            if (artistModelFromRepo == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(artistUpdateDto, artistModelFromRepo); 
            _repository.UpdateArtiste(artistModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        
        //DELETE api/Artist/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult DeleteArtist(Guid id)
        {
            var artistModelFromRepo = _repository.GetArtisteById(id);
            if (artistModelFromRepo == null)
            {
                return new NotFoundResult();
            }
            _repository.DeleteArtiste(artistModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        
    }
}
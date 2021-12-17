﻿using System;
using System.Collections.Generic;
using Application.UserCase.Music;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Music;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Music")]
    [ApiController]
    public class MusicController:ControllerBase
    {
        private readonly IMusicRepository _repository;
        private readonly IMapper _mapper;

        public MusicController(IMusicRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/Music
        [HttpGet]
        public ActionResult<IEnumerable<MusicReadDto>> GetAllMusics()
        {
            var musicItems = _repository.GetAllMusic();

            return Ok(_mapper.Map<IEnumerable<MusicReadDto>>(musicItems)); // 200 success
        }
        
        //GET api/Music/{id}
        [HttpGet("{id}", Name = "GetMusicById")]
        public ActionResult<MusicReadDto> GetMusicById(Guid id)
        {
            var musicItems = _repository.GetMusicById(id);
            if (musicItems != null)
            {
                return Ok(_mapper.Map<MusicReadDto>(musicItems));
            }
            return NotFound();
        }
        
        //POST api/Music
        [HttpPost]
        public ActionResult<MusicReadDto> CreateCommand(MusicCreateDto musicCreateDto)
        {
            var musicModel = _mapper.Map<Music>(musicCreateDto);
            _repository.CreateMusic(musicModel);
            _repository.SaveChanges();

            var musicReadDto = _mapper.Map<MusicReadDto>(musicModel);
            //return Ok(commandModel);
            return CreatedAtRoute(nameof(GetMusicById), new {Id = musicReadDto.Id}, musicReadDto); 
        }
        
        //PUT api/Music/{id}
        //used to update everything
        [HttpPut("{id}")]
        public ActionResult UpdateMusic(Guid id, MusicUpdateDto musicUpdateDto)
        {
            var musicModelFromRepo = _repository.GetMusicById(id);
            if (musicModelFromRepo == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(musicUpdateDto, musicModelFromRepo); 
            _repository.UpdateMusic(musicModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        
        //DELETE api/Music/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMusic(Guid id)
        {
            var musicModelFromRepo = _repository.GetMusicById(id);
            if (musicModelFromRepo == null)
            {
                return new NotFoundResult();
            }
            _repository.DeleteMusic(musicModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
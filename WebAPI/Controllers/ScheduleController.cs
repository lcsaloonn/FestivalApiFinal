using System;
using System.Collections.Generic;
using Application.UserCase.Schedules;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Schedules;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Schedule")]
    [ApiController]
    public class ScheduleController:ControllerBase
    {
        private readonly IScheduleRepository _repository;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/Schedule
        [HttpGet]
        public ActionResult<IEnumerable<ScheduleReadDto>> GetAllSchedules()
        {
            var scheduleItems = _repository.GetAllSchedule();

            return Ok(_mapper.Map<IEnumerable<ScheduleReadDto>>(scheduleItems)); // 200 success
        }
        
        
        //GET api/Hoodie/{id}
        [HttpGet("{id}", Name = "GetScheduleById")]
        public ActionResult<ScheduleReadDto> GetScheduleById(Guid id)
        {
            var scheduleItems = _repository.GetScheduleById(id);
            if (scheduleItems != null)
            {
                return Ok(_mapper.Map<ScheduleReadDto>(scheduleItems));
            }

            return NotFound();

        }
        
        //POST api/Hoodie
        [HttpPost]
        public ActionResult<ScheduleReadDto> CreateSchedule(ScheduleCreateDto scheduleCreateDto)
        {
            var scheduleModel = _mapper.Map<Schedules>(scheduleCreateDto);
            _repository.CreateSchedule(scheduleModel);
            _repository.SaveChanges();

            var scheduleReadDto = _mapper.Map<ScheduleReadDto>(scheduleModel);
            //return Ok(commandModel);
            return CreatedAtRoute(nameof(GetScheduleById), new {Id = scheduleReadDto.Id}, scheduleReadDto); 
        }

    }
}
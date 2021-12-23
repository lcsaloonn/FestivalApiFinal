using System;
using System.Collections.Generic;
using Application.UserCase.Tickets;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Ticket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;

        public TicketController(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/Ticket
        [HttpGet]
        public ActionResult<IEnumerable<TicketReadDto>> GetAllTickets()
        {
            var ticketItems = _repository.GetAllTickets();

            return Ok(_mapper.Map<IEnumerable<TicketReadDto>>(ticketItems)); // 200 success
        }
        //GET api/Ticket/{id}
        [HttpGet("{id}", Name = "GetTicketById")]
        public ActionResult<TicketReadDto> GetTicketById(Guid id)
        {
            var ticketItems = _repository.GetTicketById(id);
            if (ticketItems != null)
            {
                return Ok(_mapper.Map<TicketReadDto>(ticketItems));
            }

            return NotFound();

        }
        
        
        //GET api/Ticket/{name}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpGet("{name}/getTicketByName")]
        public ActionResult<TicketReadDto> GetTicketByUserName(string name)
        {
            var ticketItems = _repository.getTicketByName(name);
            if (ticketItems != null)
            {
                return Ok(_mapper.Map<TicketReadDto>(ticketItems));
            }
            return NotFound();
        }
        
        //POST api/Ticket
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<TicketReadDto> CreateTicket(TicketCreateDto ticketCreateDto)
        {
            var ticketModel = _mapper.Map<Ticket>(ticketCreateDto);
            _repository.CreateTicket(ticketModel);
            _repository.SaveChanges();

            var ticketReadDto = _mapper.Map<TicketReadDto>(ticketModel);
            //return Ok(TicketModel);
            return CreatedAtRoute(nameof(GetTicketById), new {Id = ticketReadDto.Id}, ticketReadDto); 
        }
        
        //DELETE api/hoodie/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult DeleteTicket(Guid id)
        {
            var ticketModelFromRepo = _repository.GetTicketById(id);
            if (ticketModelFromRepo == null)
            {
                return new NotFoundResult();
            }
            _repository.DeleteTicket(ticketModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

    }
}
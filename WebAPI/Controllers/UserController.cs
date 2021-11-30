using System;
using System.Collections.Generic;
using Application.UserCase.User.Dto;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserController(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }
        
        //GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllCommands()
        {
            var userItems = _userRepository.GetAll();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(userItems)); // 200 success
        }
        
        //GET api/user/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(Guid id)
        {
            var userItems = _userRepository.GetByUserId(id);
            if (userItems != null)
            {
                return Ok(_mapper.Map<UserReadDto>(userItems));
            }

            return NotFound();

        }
        
        //POST api/Users
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);
            //return Ok(commandModel);
            return CreatedAtRoute(nameof(GetUserById), new {Id = userReadDto.Id}, userReadDto); 
        }

        //DELETE api/User/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var userModelFromRepo = _userRepository.GetByUserId(id);
            if (userModelFromRepo == null)
            {
                return new NotFoundResult();
            }
            _userRepository.DeleteUser(userModelFromRepo);
            _userRepository.SaveChanges();
            return NoContent();
        }

        //PUT api/User/{id}
        //used to update everything
        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, UserUpdateDto userUpdateDto)
        {
            var userModelFromRepo = _userRepository.GetByUserId(id);
            if (userModelFromRepo == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(userUpdateDto, userModelFromRepo); 
            _userRepository.UpdateUser(userModelFromRepo);
            _userRepository.SaveChanges();
            return NoContent();
        }
    }
    


}
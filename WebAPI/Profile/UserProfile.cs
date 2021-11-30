using Application.UserCase.User.Dto;
using Domain;

namespace WebAPI.Profile
{
    public class UserProfile: AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
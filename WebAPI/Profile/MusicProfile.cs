using Application.UserCase.Music;
using Application.UserCase.User.Dto;
using Domain;

namespace WebAPI.Profile
{
    public class MusicProfile:AutoMapper.Profile
    {
        public MusicProfile()
        {
            CreateMap<Music, MusicReadDto>();
            CreateMap<MusicCreateDto, Music>();
            CreateMap<MusicUpdateDto, Music>();
            CreateMap<Music, MusicUpdateDto>();
        }
    }
}
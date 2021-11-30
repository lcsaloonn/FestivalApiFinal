using Application.UserCase.Hoodie;
using Application.UserCase.User.Dto;
using AutoMapper;
using Domain;

namespace Application.Utils
{
    public static class Mapper
    {
        private static AutoMapper.Mapper _instance;

        public static AutoMapper.Mapper GetInstance()
        {
            return _instance ??= createMapper();
        }

        public static AutoMapper.Mapper createMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Hoodies, HoodieReadDto>();
                cfg.CreateMap<HoodieCreateDto, Hoodies>();
            });
            return new AutoMapper.Mapper(config);
        }
    }
}
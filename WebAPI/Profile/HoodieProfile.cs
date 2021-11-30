using Application.UserCase.Hoodie;
using Domain;

namespace WebAPI.Profile
{
    public class HoodieProfile :AutoMapper.Profile
    {
        public HoodieProfile()
        {
            CreateMap<Hoodies, HoodieReadDto>();
            CreateMap<HoodieCreateDto, Hoodies>();
            CreateMap<HoodieUpdateDto, Hoodies>();
            CreateMap<Hoodies, HoodieUpdateDto>();
        }
    }
}
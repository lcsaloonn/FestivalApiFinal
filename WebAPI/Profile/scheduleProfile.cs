using Application.UserCase.Schedules;
using Domain;

namespace WebAPI.Profile
{
    public class ScheduleProfile:AutoMapper.Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedules, ScheduleReadDto>();
            CreateMap<ScheduleCreateDto, Schedules>();
            
        }
    }
}
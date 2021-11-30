﻿using Application.UserCase.Schedules;
using Domain;

namespace WebAPI.Profile
{
    public class scheduleProfile:AutoMapper.Profile
    {
        public scheduleProfile()
        {
            CreateMap<Schedules, ScheduleReadDto>();
            CreateMap<ScheduleCreateDto, Schedules>();
            CreateMap<ScheduleUpdateDto, Schedules>();
            CreateMap<Schedules, ScheduleUpdateDto>();
        }
    }
}
using System;

namespace Application.UserCase.Schedules
{
    public class ScheduleReadDto
    {
         public Guid Id { get; set; }
         public DateTime ScheduleStart { get; set; }
         public DateTime ScheduleEnd { get; set; }
    }
}
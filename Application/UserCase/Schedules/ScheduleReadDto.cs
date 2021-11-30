using System;

namespace Application.UserCase.Schedules
{
    public class ScheduleReadDto
    {
         public Guid Id { get; set; }
         public DateTime schedule_start { get; set; }
         public DateTime schedule_end { get; set; }
         public DateTime date_prestation { get; set; }
    }
}
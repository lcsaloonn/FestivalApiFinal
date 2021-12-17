using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Schedules
{
    public class ScheduleUpdateDto
    {
        
        [Required]
         public DateTime ScheduleStart { get; set; }

        [Required]
         public DateTime ScheduleEnd { get; set; }
        
    }
}
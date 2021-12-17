using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Schedules
{
    public class ScheduleCreateDto
    {
        
        [Required]
         public DateTime ScheduleStart { get; set; }

        [Required] public DateTime ScheduleEnd { get; set; }

        
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Schedules
{
    public class ScheduleCreateDto
    {
        
        [Required]
        [DataType(DataType.Time)] public DateTime schedule_start { get; set; }

        [Required]
        [DataType(DataType.Time)] public DateTime schedule_end { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_prestation { get; set; }
    }
}
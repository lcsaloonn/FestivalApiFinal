using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Schedules
    {
        [Key] public int Id { get; set; }

        [Required]
        public DateTime ScheduleStart { get; set; }

        [Required]
        public DateTime ScheduleEnd { get; set; }
        
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.UserCase.Music;
using Application.UserCase.Schedules;

namespace Application.UserCase.Artist
{
    public class ArtistUpdateDto
    {
        [Key]
        public int Guid { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string StageName { get; set; }
        
        [Required]
        [ForeignKey("Music")]
        public int IdMusic { get; set; }
        
        [Required]
        [ForeignKey("Schedules")]
        public int IdSchedule { get; set; }

        public virtual MusicCreateDto Music { get; set; }
        public virtual ScheduleCreateDto Schedules { get; set; }
    }
}
#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application.UserCase.Music;
using Application.UserCase.Schedules;

#pragma warning disable 8618

namespace Application.UserCase.Artist
{
    public class ArtistCreateDto
    {
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
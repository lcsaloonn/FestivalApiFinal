#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public virtual Domain.Music Music { get; set; }
        public virtual Domain.Schedules Schedules { get; set; }
    }
}
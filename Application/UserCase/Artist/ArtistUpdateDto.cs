using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UserCase.Artist
{
    public class ArtistUpdateDto
    {
        [Key]
        public int Id { get; set; }
        
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
        public virtual  Domain.Schedules Schedules { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UserCase.Artist
{
    public class ArtistUpdateDto
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Stage_Name { get; set; }
        
        [Required]
        [ForeignKey("Music")]
        public Guid? Id_Music { get; set; }
        
        [Required]
        [ForeignKey("Schedules")]
        public Guid? Id_schedule { get; set; }

        public virtual Domain.Music Music { get; set; }
        public virtual  Domain.Schedules Schedules { get; set; }
    }
}
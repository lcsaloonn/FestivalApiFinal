using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Artiste
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string StageName { get; set; }
        
        [Required]
        [ForeignKey("Music")]
        public Guid IdMusic { get; set; }
        
        [Required]
        [ForeignKey("Schedules")]
        public Guid IdSchedule { get; set; }

        public virtual Music Music { get; set; } 
        public virtual Schedules Schedules { get; set; } 
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Schedules
    {
        [Key] public Guid Id { get; set; }

        [Required]
        [Column(TypeName="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime schedule_start { get; set; }

        [Required]
        [Column(TypeName="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime schedule_end { get; set; }

       
        
        [Required]
        [Column(TypeName="Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime date_prestation { get; set; }
    }
}
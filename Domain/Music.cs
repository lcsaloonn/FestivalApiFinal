using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Music
    {
        [Key]
        public  Guid Id{ get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }
        
        
    }
}
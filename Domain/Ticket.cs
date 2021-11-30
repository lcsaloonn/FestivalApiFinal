using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Edition { get; set; }
        
    }
}
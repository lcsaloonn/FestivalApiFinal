using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Edition { get; set; }
        
        public string Nom { get; set; }
        
    }
}
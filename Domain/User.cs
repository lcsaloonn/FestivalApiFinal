using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain
{
    public class User
    {
        // GUID will create de unique ID
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Pseudo { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }


        public int? Role { get; set; } = 1;
        
        [ForeignKey("Ticket")]
        public Guid? IdTicket { get; set; }
        public virtual  Ticket Ticket { get; set; }
    }
}
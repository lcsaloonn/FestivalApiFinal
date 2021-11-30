using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Application.UserCase.User.Dto
{
    public class UserUpdateDto
    {
        public string Pseudo { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        [DefaultValue(1)] public int? Role { get; set; }
        
        [ForeignKey("Ticket")]
        public Guid? IdTicket { get; set; }
        public virtual  Ticket Ticket { get; set; }
    }
}
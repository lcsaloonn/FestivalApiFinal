using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Application.UserCase.User.Dto
{
    public class UserReadDto
    {
      
        public Guid Id { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        
        public Guid IdTicket { get; set; }
        public virtual  Ticket Ticket { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain;

namespace Application.UserCase.User.Dto
{
    public class UserCreateDto
    {
        
        
        [Required]
        [MaxLength(250)]
        public string Pseudo { get; set; }
        
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        
    }
}
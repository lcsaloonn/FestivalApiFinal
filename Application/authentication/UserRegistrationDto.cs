using System.ComponentModel.DataAnnotations;

namespace Application.authentication
{
    public class UserRegistrationDto
    {
      [Required]
      public string Pseudo { get; set; }
      
      [Required]
      public string Password {get; set;}
      
      
    }
}
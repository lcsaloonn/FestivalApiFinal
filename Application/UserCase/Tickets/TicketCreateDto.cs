using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UserCase.Tickets
{
    public class TicketCreateDto
    {
        [Required]
        public string Edition { get; set; }
        
        
        public string Nom { get; set; }
    }
}
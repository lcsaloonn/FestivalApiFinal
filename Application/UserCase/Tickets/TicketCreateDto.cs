using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Tickets
{
    public class TicketCreateDto
    {
        [Required]
        public string Edition { get; set; }
    }
}
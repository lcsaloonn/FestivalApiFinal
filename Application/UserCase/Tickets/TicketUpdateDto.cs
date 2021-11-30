using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Tickets
{
    public class TicketUpdateDto
    {
        [Required]
        public string Edition { get; set; }
    }
}
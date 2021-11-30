using Application.UserCase.Tickets;
using Domain;

namespace WebAPI.Profile
{
    public class TicketProfile:AutoMapper.Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketReadDto>();
            CreateMap<TicketCreateDto, Ticket>();
            
        }
    }
}
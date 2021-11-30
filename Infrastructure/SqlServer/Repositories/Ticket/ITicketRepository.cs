using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Ticket
{
    public interface ITicketRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.Ticket> GetAllTickets();
        Domain.Ticket GetTicketById(Guid id);
        void CreateTicket(Domain.Ticket ticket);
        void UpdateTicket(Domain.Ticket ticket);
        void DeleteTicket(Domain.Ticket ticket);
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.Ticket
{
    public class TicketRepository : ITicketRepository
    {
        private readonly Context _context;

        public TicketRepository(Context context)
        {
            _context = context;
        }
        
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.Ticket> GetAllTickets()
        {
            return _context.Tickets.ToList();
        }

        public Domain.Ticket GetTicketById(Guid id)
        {
            return _context.Tickets.FirstOrDefault(p => p.Id == id);
        }
        
        public Domain.Ticket getTicketByName(string name)
        {
             return _context.Tickets.FirstOrDefault(p => p.Nom == name);
        }
        

        public void CreateTicket(Domain.Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            _context.Tickets.Add(ticket);
        }

        public void UpdateTicket(Domain.Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void DeleteTicket(Domain.Ticket ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            _context.Tickets.Remove(ticket);
        }
    }
}
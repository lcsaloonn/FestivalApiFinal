using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.Hoodies
{
    public class HoodieRepository : IHoodiesRepository
    {
        private readonly Context _context;

        public HoodieRepository(Context context)
        {
            _context = context;
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.Hoodies> GetAllHoodies()
        {
            return _context.Hoodies.ToList();
        }

        public Domain.Hoodies GetHoodieById(Guid id)
        {
            return _context.Hoodies.FirstOrDefault(p => p.Id == id);
        }

        public Domain.Hoodies CreateHoodie(Domain.Hoodies hoodie)
        {
            if (hoodie == null)
            {
                throw new ArgumentNullException(nameof(hoodie));
            }

            _context.Hoodies.Add(hoodie);
            return hoodie;
        }

        public void UpdateHoodie(Domain.Hoodies command)
        {
            
        }

        public void DeleteHoodie(Domain.Hoodies hoodie)
        {
            if (hoodie == null)
            {
                throw new ArgumentNullException(nameof(hoodie));
            }

            _context.Hoodies.Remove(hoodie);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.Artiste
{
    public class ArtistRepository:IArtistRepository
    {
        private readonly Context _context;

        public ArtistRepository(Context context)
        {
            _context = context;
        }
        
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.Artiste> GetAllArtistes()
        {
            return _context.Artistes.ToList();
        }

        public Domain.Artiste GetArtisteById(Guid id)
        {
            return _context.Artistes.FirstOrDefault(p => p.Id == id);
        }

        public Domain.Artiste CreateArtiste(Domain.Artiste artiste)
        {
            if (artiste == null)
            {
                throw new ArgumentNullException(nameof(artiste));
            }

            _context.Artistes.Add(artiste);
            return artiste;
        }

        public void UpdateArtiste(Domain.Artiste artiste)
        {
            
        }

        public void DeleteArtiste(Domain.Artiste artiste)
        {
            if (artiste == null)
            {
                throw new ArgumentNullException(nameof(artiste));
            }

            _context.Artistes.Remove(artiste);
        }
    }
}
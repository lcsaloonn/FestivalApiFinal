using System;
using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Artiste
{
    public interface IArtistRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.Artiste> GetAllArtistes();
        Domain.Artiste GetArtisteById(Guid id);
        Domain.Artiste CreateArtiste(Domain.Artiste artiste);
        void UpdateArtiste(Domain.Artiste artiste);
        void DeleteArtiste(Domain.Artiste artiste);

    }
}
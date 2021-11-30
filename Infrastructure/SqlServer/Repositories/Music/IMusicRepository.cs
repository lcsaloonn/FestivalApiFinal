using System;
using System.Collections.Generic;

namespace Infrastructure.SqlServer.Repositories.Music
{
    public interface IMusicRepository
    {
        bool SaveChanges();
        IEnumerable<Domain.Music> GetAllMusic();
        Domain.Music GetMusicById(Guid id);
        void CreateMusic(Domain.Music music);
        void UpdateMusic(Domain.Music music);
        void DeleteMusic(Domain.Music music);
    }
}
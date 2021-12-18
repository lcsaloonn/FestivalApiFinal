using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.SqlServer.Data;

namespace Infrastructure.SqlServer.Repositories.Music
{
    public class MusicRepository:IMusicRepository
    {
        private readonly Context _context;

        public MusicRepository(Context context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }

        public IEnumerable<Domain.Music> GetAllMusic()
        {
            return _context.Musics.ToList();
        }

        public Domain.Music GetMusicById(int id)
        {
            return _context.Musics.FirstOrDefault(p => p.Id == id);
        }

        public void CreateMusic(Domain.Music music)
        {
            if (music == null)
            {
                throw new ArgumentNullException(nameof(music));
            }

            _context.Musics.Add(music);
        }

        public void UpdateMusic(Domain.Music music)
        {
            
        }

        public void DeleteMusic(Domain.Music music)
        {
            if (music == null)
            {
                throw new ArgumentNullException(nameof(music));
            }

            _context.Musics.Remove(music);
        }
    }
}
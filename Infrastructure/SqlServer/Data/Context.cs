using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Data
{
    public class Context:IdentityDbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            
        }
        
        /**
         * the DbSet property will tell ef (entity framework) that we have a
         * table that needs to be created if doesnt exist
         */
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Music> Musics { get; set; }
        
        public virtual DbSet<Hoodies> Hoodies { get; set; }
        public virtual DbSet<Artiste> Artistes { get; set; }
    }
}
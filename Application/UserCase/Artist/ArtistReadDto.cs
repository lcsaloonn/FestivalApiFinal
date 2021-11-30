using System;

namespace Application.UserCase.Artist
{
    public class ArtistReadDto
    {
        
        public Guid Id { get; set; }
        public string Stage_Name { get; set; }
        public Guid Id_Music { get; set; }
        public Guid Id_schedule { get; set; }
        public virtual Domain.Music Music { get; set; }
        public virtual  Domain.Schedules Schedules { get; set; }
    }
}
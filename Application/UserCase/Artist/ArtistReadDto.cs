using System;

namespace Application.UserCase.Artist
{
    public class ArtistReadDto
    {
        
        public  Guid Id{ get; set; }
        public string StageName { get; set; }
        public int IdMusic { get; set; }
        public int IdSchedule { get; set; }
        //public virtual Domain.Music Music { get; set; }
        //public virtual  Domain.Schedules Schedules { get; set; }
    }
}
using System;

namespace Application.UserCase.Music
{
    public class MusicReadDto
    {
        
        public  Guid Id{ get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
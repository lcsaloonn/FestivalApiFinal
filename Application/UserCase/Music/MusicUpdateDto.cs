using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Music
{
    public class MusicUpdateDto
    {
         
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Application.UserCase.Artist
{
    public class ArtistCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Stage_Name { get; set; }
    }
}
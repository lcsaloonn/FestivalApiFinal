using Application.UserCase.Artist;
using Domain;

namespace WebAPI.Profile
{
    public class ArtistProfile:AutoMapper.Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artiste, ArtistReadDto>();
            CreateMap<ArtistCreateDto, Artiste>();
            CreateMap<ArtistUpdateDto, Artiste>();
            CreateMap<Artiste, ArtistUpdateDto>();
        }
    }
}
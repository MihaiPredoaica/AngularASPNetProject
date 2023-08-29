using AnguilarTutorialAPI.DTOs;
using AnguilarTutorialAPI.Entity;
using AnguilarTutorialAPI.Extensions;
using AutoMapper;

namespace AnguilarTutorialAPI.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            CreateMap<AppUser, MemberDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDTO>();
        }
    }
}

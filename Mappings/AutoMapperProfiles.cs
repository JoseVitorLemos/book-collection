using book_collection.Models;
using book_collection.Dto;
using AutoMapper;

namespace book_collection.Mappings
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Profiles, ProfileDto>().ReverseMap();
      CreateMap<ProfileDto, ResponseProfileDto>().ReverseMap();
      CreateMap<ImageProfile, ImageProfileDto>()
        .ForMember(dest => dest.profile_id,
          map => map.MapFrom(prop => prop.profilesId))
        .ReverseMap();
      CreateMap<Profiles, ResponseProfileDto>().ReverseMap();
      CreateMap<PutProfileDto, Profiles>().ReverseMap();
    }
  }
}

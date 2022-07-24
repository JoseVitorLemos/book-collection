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
    }
  }
}

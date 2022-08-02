using book_collection.Models;
using book_collection.Dto;
using AutoMapper;

namespace book_collection.Mappings
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<Profiles, CreateProfileDto>().ReverseMap();
      CreateMap<CreateProfileDto, ResponseProfileDto>().ReverseMap();
      CreateMap<ImageProfile, ImageProfileDto>().ReverseMap();
      CreateMap<Profiles, ResponseProfileDto>().ReverseMap();
      CreateMap<PutProfileDto, Profiles>().ReverseMap();
    }
  }
}

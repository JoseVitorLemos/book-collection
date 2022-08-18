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
      CreateMap<ForgotPasswordDto, ResetPassword>().ReverseMap();
      CreateMap<ResetForgotPasswordDto, Profiles>().ReverseMap();
      CreateMap<ResetPasswordDto, Profiles>().ReverseMap();
    }
  }
}

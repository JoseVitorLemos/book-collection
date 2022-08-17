using Microsoft.AspNetCore.Mvc;
using book_collection.Interface;
using book_collection.Models;
using book_collection.Dto;
using book_collection.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using book_collection.Services.Auth;
using book_collection.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace book_collection.Controllers 
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  [Produces("application/json")]
  public class ProfileController : ControllerBase
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISmtpService _smtpService;
    private readonly IJwtService _jwtService;
    private readonly Guid _profileId;

    public ProfileController (
      IUnitOfWork unitOfWork,
      IMapper mapper,
      ISmtpService smtpHelper,
      IJwtService jwtService,
      IProfilesRepository profilesRepository,
      IAuth auth)
    {
      this._mapper = mapper;
      this._smtpService = smtpHelper;
      this._jwtService = jwtService;
      this._unitOfWork = unitOfWork;
      this._profileId = auth.GetUserId();
    }

    [HttpGet("image")]
    public async Task<ActionResult<ImageProfile>> FindImage()
    {
      var imageProfile = await _unitOfWork.ImageProfileRepositoy
        .WhereAsync(image => image.profilesId == this._profileId);
      if(imageProfile == null) return NoContent();
      return imageProfile;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseProfileDto>> GetProfile()
    {
      var profile = await _unitOfWork.ProfilesRepository.GetById(this._profileId);
      return _mapper.Map<ResponseProfileDto>(profile);
    }

    [HttpPost("image")]
    public async Task<ActionResult> PostImageProfile([FromForm] ImageProfileDto imageProfileDto)
    {
      try 
      {
        var image = await _unitOfWork.ImageProfileRepositoy
          .WhereAsync(image => image.profilesId == this._profileId);

        if (image != null) return BadRequest(new { message = "can only one profile image" });

        if (imageProfileDto.image != null)
        {
          var ms = new MemoryStream();
          await imageProfileDto.image.CopyToAsync(ms);
          var fileBytes = ms.ToArray();

          var imageProfile = _mapper.Map<ImageProfile>(imageProfileDto);
          imageProfile.image_byte = fileBytes;
          imageProfile.profilesId = this._profileId;

          await _unitOfWork.ImageProfileRepositoy.CreateAsync(imageProfile);
        }
        else
        {
          return BadRequest();
        }
        return Ok("Image saved successfully");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, "error saving image");
      }
    }

    [HttpPut]
    public async Task<ActionResult<string>> Put([FromBody] PutProfileDto profileDto)
    {
      try
      {
        var profile = await _unitOfWork.ProfilesRepository.GetById(this._profileId);

        if (profile == null) return Unauthorized(new { message = "Unauthorized invalid id" });

        if (profileDto.name == null) profileDto.name = profile.name;
        if (profileDto.birth_date == DateTime.Parse("0001-01-01T00:00:00")) profileDto.birth_date = profile.birth_date;

        var profileChanges = _mapper.Map(profileDto, profile);

        await _unitOfWork.ProfilesRepository.UpdateAsync(this._profileId, profileChanges);
        return Ok("profile changes saved successfully");
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return StatusCode(StatusCodes.Status500InternalServerError, "error changes profile");
      }
    }

    [HttpPut("image")]
    public async Task<ActionResult> UpdateImageProfile([FromForm] ImageProfileDto imageProfileDto)
    {
      try 
      {
        if (imageProfileDto.image != null)
        {
          var ms = new MemoryStream();
          await imageProfileDto.image.CopyToAsync(ms);
          var fileBytes = ms.ToArray();

          var imageProfile = await _unitOfWork.ImageProfileRepositoy
            .WhereAsync(image => image.profilesId == this._profileId);

          if (imageProfile == null) return BadRequest(new { message = "no image registered" });

          var imageChange = _mapper.Map(imageProfileDto, imageProfile);
          imageChange.image_byte = fileBytes;

          await _unitOfWork.ImageProfileRepositoy.UpdateAsync(imageProfile.id, imageChange);
        }
        else
        {
          return BadRequest(new { message = "image are not provided" });
        }
        return Ok("Image updated successfully");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, "error saving image");
      }
    }
  }
}

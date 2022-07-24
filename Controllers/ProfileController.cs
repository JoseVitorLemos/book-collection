using Microsoft.AspNetCore.Mvc;
using book_collection.Models;
using book_collection.Dto;
using book_collection.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace book_collection.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;
  private readonly IMapper _mapper;

  public ProfileController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
  {
    this._context = context;
    this._webHostEnvironment = webHostEnvironment;
    this._mapper = mapper;
  }

  [HttpGet("image/{id}")]
  public async Task<ActionResult<ImageProfile>> Get(int id)
  {
    return await _context.ImageProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.id == id);
  }

  [HttpPost]
  public ActionResult<ResponseProfileDto> Post([FromBody] ProfileDto profile)
  {
    try 
    {
      if (profile.password != profile.passwordConfirmation) 
      {
        return BadRequest("password and passwordConfirmation are not the same");
      }

      var profileData = _mapper.Map<Profiles>(profile);
      
      _context.Profiles.Add(profileData);
      _context.SaveChanges();

      return Ok(_mapper.Map<ResponseProfileDto>(profile));
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, "error when registering a new profile");
    }
  }

  [HttpPost("image")]
  public async Task<ActionResult> PostImageProfile([FromForm] ImageProfileDto imageProfileDto)
  {
    try 
    {
      var images = await _context.ImageProfiles
        .Where(i => i.profilesId == imageProfileDto.profile_id)
        .AsNoTracking()
        .ToListAsync();

      if (images.Count() != 0) return BadRequest("profile image can only one");

      if (imageProfileDto.image != null)
      {
        var ms = new MemoryStream();
        await imageProfileDto.image.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
       
        var imageProfile = _mapper.Map<ImageProfile>(imageProfileDto);
        imageProfile.image_byte = fileBytes;

        _context.ImageProfiles.Add(imageProfile);
        _context.SaveChanges();
      }
      else
      {
        return BadRequest();
      }
      return Ok("Image saved successfully");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, "error saving image");
    }
  }
}

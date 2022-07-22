using Microsoft.AspNetCore.Mvc;
using book_collection.Models;
using book_collection.Dto;
using book_collection.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace book_collection.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public ProfileController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
  {
    this._context = context;
    this._webHostEnvironment = webHostEnvironment;
  }

  [HttpGet]
  public async Task<ActionResult<ImageProfile>> Get(int id)
  {
    return await _context.ImageProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.id == id);
  }

  [HttpPost]
  public ActionResult<ProfileDto> Post([FromBody] ProfileDto profile)
  {
    try 
    {
      if (profile.password != profile.passwordConfirmation) 
      {
        return BadRequest("password and passwordConfirmation are not the same");
      }

      var profileData = new Profile {
        cpf = profile.cpf,
        name = profile.name,
        birth_date = profile.birth_date,
        email = profile.email,
        password = profile.password
      };

      _context.Profiles.Add(profileData);
      _context.SaveChanges();

      return Ok(new { id = profileData.id, profile });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "error when registering a new profile");
    }
  }

  [HttpPost("image/{id}")]
  public async Task<ActionResult> PostImageProfile(int id, [FromForm] ImageProfileDto imageProfileDto)
  {
    try 
    {
      var images = await _context.ImageProfiles.AsNoTracking().ToListAsync();

      if (images.Count() != 0) return BadRequest("profile image can only one");

      if (imageProfileDto.image != null && images.Count() == 0)
      {
        var ms = new MemoryStream();
        await imageProfileDto.image.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
       
        var imageProfile = new ImageProfile {
          title = imageProfileDto.image.FileName,
          image_byte = fileBytes,
          ProfileId = imageProfileDto.ProfileId
        };
        _context.ImageProfiles.Add(imageProfile);
        _context.SaveChanges();
      }
      else
      {
        return BadRequest();
      }
      return Ok("Saved success");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, "error saving image");
    }
  }
}

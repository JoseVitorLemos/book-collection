using Microsoft.AspNetCore.Mvc;
using book_collection.Models;
using book_collection.Dto;
using book_collection.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace book_collection.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IWebHostEnvironment _webHostEnvironment;

  public ProfilesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
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
}

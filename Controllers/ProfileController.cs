using Microsoft.AspNetCore.Mvc;
using book_collection.Interface;
using book_collection.Models;
using book_collection.Dto;
using book_collection.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using book_collection.Helpers.Bcrypt;
using book_collection.Repositories;
using AutoMapper;

namespace book_collection.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
  private readonly AppDbContext _context;
  private readonly IMapper _mapper;
  private readonly ISmtpService _smtpService;
  private readonly IJwtService _jwtService;
  private readonly IProfilesRepository _profileRepositories;

  public ProfileController (
    AppDbContext context, 
    IMapper mapper,
    ISmtpService smtpHelper,
    IJwtService jwtService,
    IProfilesRepository profilesRepository)
  {
    this._context = context;
    this._mapper = mapper;
    this._smtpService = smtpHelper;
    this._jwtService = jwtService;
    this._profileRepositories = profilesRepository;
  }

  [HttpPost("/login")]
  public async Task<ActionResult<dynamic>> LoginAsync([FromBody] LoginDto model)
  {
    var user = await _profileRepositories.Get(model); 

    if (user == null) return NotFound(new { message = "user or password invalid" });

    var validatePassword = Bcrypt.ValidatePassword(model.password, user.password);

    if (!validatePassword) return BadRequest(new { message = "invalid password" });

    var token = _jwtService.GenerateToken(user.email);

    var image = user.ImageProfiles;

    return new 
    {
      user = new {
        id = user.id,
        name = user.name,
        email = user.email,
        image = image.Count == 0 ? null : image.ElementAt(0).image_byte
      },
      token
    };
  }

  [HttpGet("image/{id}")]
  public async Task<ActionResult<ImageProfile>> Get(int id)
  {
    return await _context.ImageProfiles.AsNoTracking().FirstOrDefaultAsync(p => p.id == id);
  }

  [HttpGet]
  public async Task<ActionResult<ResponseProfileDto>> GetProfile(int id)
  {
    var profile = await _context.Profiles.Where(p => p.id == id).AsNoTracking().FirstOrDefaultAsync();
    return _mapper.Map<ResponseProfileDto>(profile);
  }

  [HttpPost("/signup")]
  public ActionResult<ResponseProfileDto> Signup([FromBody] CreateProfileDto model)
  {
    try 
    {
      var profile = _mapper.Map<Profiles>(model);

      var salt = 12;

      profile.password = Bcrypt.HashPassword(profile.password, salt);

      _context.Profiles.Add(profile);
      _context.SaveChanges();

      _smtpService.SendEmail(profile.email, "Confirmation email", "Use this link to confirm email");

      return Ok(_mapper.Map<ResponseProfileDto>(model));
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

  [HttpPut("{id}")]
  public async Task<ActionResult<CreateProfileDto>> Put(int id, [FromBody] CreateProfileDto profileDto)
  {
    try
    {
      var profile = await _context.Profiles.AsNoTracking().Where(p => p.id == id).FirstOrDefaultAsync();
      if (profile == null) return NotFound();

      if (profileDto.name == null) profileDto.name = profile.name;
      if (profileDto.birth_date == DateTime.Parse("0001-01-01T00:00:00")) profileDto.birth_date = profile.birth_date;

      var profileChanges = _mapper.Map(profileDto, profile);

      _context.Entry(profileChanges).State = EntityState.Modified;
      _context.SaveChanges();
      return Ok("profile changes saved successfully");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError, "error changes profile");
    }
  }
}

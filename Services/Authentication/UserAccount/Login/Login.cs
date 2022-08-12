using Microsoft.AspNetCore.Mvc;
using book_collection.Models;
using book_collection.Interface;
using book_collection.Dto;
using book_collection.Helpers.Bcrypt;
using book_collection.Services.Auth;
using System.Security.Claims;
using book_collection.Repositories;
using AutoMapper;

namespace book_collection.Controllers
{
  [ApiController]
  [Route("account")]
  public class LoginController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly IAuth _auth;

    public LoginController(
      IMapper mapper,
      IUnitOfWork unitOfWork,
      IAuth auth,
      IJwtService jwtService)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
      _auth = auth;
      _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<dynamic>> LoginAsync([FromBody] LoginDto model)
    {
      var user = await _unitOfWork.ProfilesRepository.Login(model);

      if (user == null) return NotFound(new { message = "user or password invalid" });

      var validatePassword = Bcrypt.ValidatePassword(model.password, user.password);

      if (!validatePassword) return BadRequest(new { message = "invalid password" });

      var token = _jwtService.GenerateToken(user);

      var image = user.ImageProfiles;

      return new 
      {
        user = new {
          id = user.id,
            name = user.name,
            email = user.email,
            image = image.Count == 0 ? null : image.Single().image_byte
        },
        token
      };
    }
  }
}


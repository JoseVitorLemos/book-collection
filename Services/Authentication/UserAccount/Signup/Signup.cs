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
  public class SignupController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuth _auth;

    public SignupController(IMapper mapper, IUnitOfWork unitOfWork, IAuth auth)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
      _auth = auth;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<ResponseProfileDto>> Signup([FromBody] CreateProfileDto model)
    {
      try 
      {
        var profile = _mapper.Map<Profiles>(model);

        var userExist = await _unitOfWork.ProfilesRepository.OrWhere(profile);

        if (userExist) 
          return BadRequest(new { message = "email or cpf already registered" });

        var salt = 12;

        profile.password = Bcrypt.HashPassword(profile.password, salt);

        _unitOfWork.ProfilesRepository.Add(profile);
        _unitOfWork.Commit();

        return Ok(_mapper.Map<ResponseProfileDto>(model));
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return StatusCode(StatusCodes.Status500InternalServerError, "error when registering a new profile");
      }
    }
  }
}

using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using book_collection.Helpers.Bcrypt;
using book_collection.Models;
using book_collection.Interface;
using book_collection.Dto;
using book_collection.Repositories;
using AutoMapper;

namespace book_collection.Controllers
{
  [ApiController]
  [Route("account")]
  public class ForgotPassowrdController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    private readonly ISmtpService _smtpService;
    private readonly IMapper _mapper;
    private readonly string _host;
    private readonly Guid _profileId;

    public ForgotPassowrdController(
      IUnitOfWork unitOfWork,
      IJwtService jwtService,
      IConfiguration configuration,
      ISmtpService smtpService,
      IMapper mapper,
      IAuth auth)
    {
      _unitOfWork = unitOfWork;
      _jwtService = jwtService;
      _configuration = configuration;
      _smtpService = smtpService;
      _mapper = mapper;
      _host = _configuration["Uri"];
      _profileId = auth.GetUserId();
    }

    [SwaggerOperation(Tags = new[] { "Account" })]
    [HttpPost("forgot-password")]
    public async Task<ActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDto model)
    {
      try {
        var profile = await _unitOfWork.ProfilesRepository.WhereAsync(p => p.email == model.email);
        if (profile != null) 
        {
          int expireMinutes = 30;
          var token = _jwtService.CustomToken(profile, expireMinutes);

          var exists = await _unitOfWork.ResetPasswordRepositoy.WhereAsync(x => x.profilesId == profile.id);

          var newResetPassword = _mapper.Map(model, exists);
          newResetPassword.profilesId = profile.id; 

          if (exists == null) { 
            await SendEmail(profile.email, token);
            await _unitOfWork.ResetPasswordRepositoy.CreateAsync(newResetPassword);
          } else
          {
            await SendEmail(profile.email, token);
            await _unitOfWork.ResetPasswordRepositoy.UpdateAsync(exists.id, newResetPassword);
          }

          return Ok(new { message = "(30 min Reset will expires). Password reset request has been sent to the "+
          "registered mailbox if an account exists" });
        }
        else
        {
          return BadRequest("user email not registered");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, "internal server error");
      }
    }

    [SwaggerOperation(Tags = new[] { "Account" })]
    [HttpPut("recover-forgotten-password/{token}")]
    public async Task<ActionResult<dynamic>> RecoverForgotPasswordAsync([FromBody] ResetForgotPasswordDto resetPassword, string token)
    {
      var isValid = _jwtService.ValidateToken(token);
      if (!isValid) return BadRequest(new { message = "invalid or expired token" });

      var resetPass = await _unitOfWork.ResetPasswordRepositoy.WhereAsync(p => p.email == resetPassword.email);
      if (resetPass == null) return BadRequest(new { message = "request to retrieve not found" });

      var profile = await _unitOfWork.ProfilesRepository.GetById(resetPass.profilesId);

      var salt = 12;
      profile.password = Bcrypt.HashPassword(resetPassword.password, salt);

      await _unitOfWork.BeginTransactionAsync();

      try 
      {
        await _unitOfWork.ProfilesRepository.UpdateAsync(profile.id, profile);
        await _unitOfWork.ResetPasswordRepositoy.DeleteAsync(resetPass.id);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        await _unitOfWork.RollbackAsync();
        return BadRequest(new { message = "error in recover password" });
      }

      await _unitOfWork.CommitAsync();
      return Ok(new { message = "successful password change" });
    }

    [Authorize]
    [SwaggerOperation(Tags = new[] { "Account" })]
    [HttpPut("reset-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordDto resetPassword)
    {
      try
      {
        var entity = await _unitOfWork.ProfilesRepository.GetById(this._profileId);

        var validatePassword = Bcrypt.ValidatePassword(resetPassword.oldPassword, entity.password);
        if (!validatePassword) return BadRequest(new { message = "invalid password" });

        var salt = 12;
        var HashPassword = Bcrypt.HashPassword(resetPassword.password, salt);

        var profile = _mapper.Map(resetPassword, entity);
        profile.password = HashPassword;

        await _unitOfWork.ProfilesRepository.UpdateAsync(profile.id, profile);
        return Ok(new { message = "successful password change" });
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return StatusCode(500, new { message = "there was an error resetting the password" });
      }
    }

    private async Task<ActionResult<bool>> SendEmail(string email, string token)
    {
      var success = await _smtpService.SendEmail(
        email,
        "Support Book Collection",
        "Retrieve password link <a href=" + _host+ "recover-forgotten-password/" + token + ">Uri</a>");

      if (!success) 
        return BadRequest(new { message = "internal email sender error" });
      return true;
    }
  }
}


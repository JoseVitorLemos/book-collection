using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using book_collection.Interface;
using System.Text;

namespace book_collection.Services
{
  public class JwtService : IJwtService
  {
    private readonly IConfiguration Configuration;
    private readonly string _secret;

    public JwtService(IConfiguration configuration)
    {
      this._secret = configuration["JWT:secret"];
    }
    public string GenerateToken(string email)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(this._secret);
      var descriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.Email, email)
        }),
        Expires = DateTime.UtcNow.AddHours(8),
        SigningCredentials = new SigningCredentials (
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var token = tokenHandler.CreateToken(descriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}

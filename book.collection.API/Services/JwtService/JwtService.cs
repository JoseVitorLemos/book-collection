using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Security.Claims;
using book_collection.Interface;
using book_collection.Models;
using System.Text;

namespace book_collection.Services
{
  public class JwtService : IJwtService
  {
    private readonly IConfiguration Configuration;
    private readonly string _secret;
    private readonly int _expireHours;
    private readonly string _audience;

    public JwtService(IConfiguration configuration)
    {
      this.Configuration = configuration;
      this._secret = Configuration["JWT:secret"];
      this._expireHours = int.Parse(Configuration["JWT:TokenConfiguration:expireHours"]);
      this._audience = Configuration["JWT:TokenConfiguration:audience"];
    }

    public string GenerateToken(Profiles entity)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_secret);
      var descriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, entity.id.ToString())
        }),
        Expires = DateTime.UtcNow.AddHours(_expireHours),
        SigningCredentials = new SigningCredentials (
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var token = tokenHandler.CreateToken(descriptor);
      return tokenHandler.WriteToken(token);
    }

    public string CustomToken(Profiles entity, int expire)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_secret);
      var descriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, entity.id.ToString())
        }),
        Expires = DateTime.UtcNow.AddMinutes(expire),
        SigningCredentials = new SigningCredentials (
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };
      var token = tokenHandler.CreateToken(descriptor);
      return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
      try {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        SecurityToken validatedToken;
        IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    private TokenValidationParameters GetValidationParameters()
    {
      return new TokenValidationParameters()
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secret)),
        ValidateIssuer = false,
        ValidateAudience = false     
      };
    }
  }
}

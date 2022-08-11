using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using book_collection.Interface;

namespace book_collection.Services.Auth
{
  public class Auth : IAuth
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Auth(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetUserId()
    {
      return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
  }
}


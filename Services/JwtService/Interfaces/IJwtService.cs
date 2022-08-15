using book_collection.Models;
using Microsoft.IdentityModel.Tokens;

namespace book_collection.Interface
{
  public interface IJwtService
  {
    string GenerateToken(Profiles entity);
    string CustomToken(Profiles entity, int expire);
    bool ValidateToken(string token);
  }
}

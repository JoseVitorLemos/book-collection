using book_collection.Models;

namespace book_collection.Interface
{
  public interface IJwtService
  {
    string GenerateToken(Profiles body);
  }
}

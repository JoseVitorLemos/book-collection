using book_collection.Dto;

namespace book_collection.Interface
{
  public interface IJwtService
  {
    string GenerateToken(string email);
  }
}

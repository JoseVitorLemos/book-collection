using book_collection.Models;
using book_collection.Dto;

namespace book_collection.Interface
{
  public interface IProfilesRepository
  {
    Task<Profiles> Get(LoginDto login);
  }
}

using book_collection.Models;
using book_collection.Dto;
using book_collection.Repositories;

namespace book_collection.Interface
{
  public interface IProfilesRepository : IRepository<Profiles>
  {
    Task<Profiles> Login(LoginDto login);

    bool OrWhere(Profiles model);
  }
}

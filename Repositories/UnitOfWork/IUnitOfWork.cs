using book_collection.Interface;

namespace book_collection.Repositories
{
  public interface IUnitOfWork
  {
    IProfilesRepository ProfilesRepository { get; }
    IImageProfileRepositoy ImageProfileRepositoy { get; }

    void Commit();
  }
}

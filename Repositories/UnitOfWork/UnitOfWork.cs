using book_collection.Interface;
using book_collection.Context;

namespace book_collection.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    public AppDbContext _context; 
    private ProfilesRepository _profileRepository;
    private ImageProfileRepositoy _imageProfileRepository;

    public UnitOfWork(AppDbContext context)
    {
      _context = context;
    }

    public IProfilesRepository ProfilesRepository {
      get 
      {
        return _profileRepository = _profileRepository ?? new ProfilesRepository(_context);
      }
    }

    public IImageProfileRepositoy ImageProfileRepositoy {
      get 
      {
        return _imageProfileRepository = _imageProfileRepository ?? new ImageProfileRepositoy(_context);
      }
    }

    public async void Commit()
    {
      try
      {
        _context.SaveChangesAsync().Wait();
      } 
      catch(Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public async void Dispose()
    {
      _context.Dispose();
    }
  }
}

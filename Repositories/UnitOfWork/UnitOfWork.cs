using Microsoft.EntityFrameworkCore.Storage;
using book_collection.Interface;
using book_collection.Context;

namespace book_collection.Repositories
{
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    public AppDbContext _context; 
    private IDbContextTransaction _transaction;

    private ProfilesRepository _profileRepository;
    private ImageProfileRepositoy _imageProfileRepository;
    private ResetPasswordRepositoy _resetPassowrdRepository;


    public UnitOfWork(AppDbContext context)
    {
      _context = context;
    }

    public IProfilesRepository ProfilesRepository {
      get 
      {
        return _profileRepository ??= new ProfilesRepository(_context);
      }
    }

    public IImageProfileRepositoy ImageProfileRepositoy {
      get 
      {
        return _imageProfileRepository ??= new ImageProfileRepositoy(_context);
      }
    }

    public IResetPasswordRepositoy ResetPasswordRepositoy {
      get 
      {
        return _resetPassowrdRepository ??= new ResetPasswordRepositoy(_context);
      }
    }

    public async Task CommitAsync()
    {
      await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
      await _transaction.RollbackAsync();
    }

    public async Task BeginTransactionAsync()
    {
      _transaction = await _context.Database.BeginTransactionAsync(); 
    }

    public void Dispose()
    {
      if (_context != null)
      {
        _context.Dispose();
      }
    }
  }
}

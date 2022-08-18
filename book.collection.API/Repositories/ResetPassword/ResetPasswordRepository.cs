using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;

namespace book_collection.Repositories
{
  public class ResetPasswordRepositoy : Repository<ResetPassword>, IResetPasswordRepositoy
  {
    public ResetPasswordRepositoy(AppDbContext context)
    {
      _context = context;
      _dbSet = context.ResetPasswords;
    }
  }
}

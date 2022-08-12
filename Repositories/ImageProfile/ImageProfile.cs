using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;

namespace book_collection.Repositories
{
  public class ImageProfileRepositoy : Repository<ImageProfile>, IImageProfileRepositoy
  {
    public ImageProfileRepositoy(AppDbContext context)
    {
      _context = context;
      _dbSet = context.ImageProfiles;
    }
  }
}

using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;

namespace book_collection.Repositories
{
  public class ImageProfileRepositoy : Repository<ImageProfile>, IImageProfileRepositoy
  {
    private readonly AppDbContext _context;

    public ImageProfileRepositoy(AppDbContext context) : base(context)
    {
      _context = context;
    }
  }
}

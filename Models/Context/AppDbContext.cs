using Microsoft.EntityFrameworkCore;

namespace book_collection.Context
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}
  }
}

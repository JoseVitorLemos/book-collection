using Microsoft.EntityFrameworkCore;
using book_collection.Models;

namespace book_collection.Context
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<PublishingCompanie> PublishingCompanies { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ImageAuthor> ImageAuthors { get; set; }
    public DbSet<ImageBook> ImageBooks { get; set; }
    public DbSet<ImageProfile> ImageProfiles { get; set; }
  }
}

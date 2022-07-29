using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;
using book_collection.Dto;
using Microsoft.EntityFrameworkCore;

namespace book_collection.Repositories
{
  public class ProfilesRepository : IProfilesRepository
  {
    private readonly AppDbContext _context;

    public ProfilesRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<Profiles> Get(LoginDto login)
    {
      var profile = _context.Profiles.Include(x=> x.ImageProfiles).FirstOrDefault(p => p.email == login.email) ?? 
        _context.Profiles.Include(x => x.ImageProfiles).FirstOrDefault(p => p.cpf == login.cpf);
      return profile;
    }
  }
}

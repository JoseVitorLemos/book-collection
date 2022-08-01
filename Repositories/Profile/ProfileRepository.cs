using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;
using book_collection.Dto;
using Microsoft.EntityFrameworkCore;

namespace book_collection.Repositories
{
  public class ProfilesRepository : Repository<Profiles>, IProfilesRepository
  {
    private readonly AppDbContext _context;

    public ProfilesRepository(AppDbContext context) : base(context)
    {
      _context = context;
    }

    public async Task<Profiles> Login(LoginDto login)
    {
      var profile = _context.Profiles.Include(x=> x.ImageProfiles).FirstOrDefault(p => p.email == login.email) ?? 
        _context.Profiles.Include(x => x.ImageProfiles).FirstOrDefault(p => p.cpf == login.cpf);
      return profile;
    }

    public bool OrWhere(Profiles model)
    {
      var profile = _context.Profiles.AsNoTracking().Where(p => (p.email == model.email) || (p.cpf == model.cpf)).ToList();
      return profile.Count > 0 ? true : false;
    }
  }
}

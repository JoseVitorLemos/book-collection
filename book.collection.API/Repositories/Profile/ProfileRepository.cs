using book_collection.Models;
using book_collection.Interface;
using book_collection.Context;
using book_collection.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace book_collection.Repositories
{
  public class ProfilesRepository : Repository<Profiles>, IProfilesRepository
  {
    public ProfilesRepository(AppDbContext context)
    {
      _context = context;
      _dbSet = context.Profiles;
    }
    
    public async Task<Profiles> Login(LoginDto login)
    {
      var profile = await _dbSet.Include(x=> x.ImageProfiles).FirstOrDefaultAsync(p => p.email == login.email) ?? 
        await _dbSet.Include(x => x.ImageProfiles).FirstOrDefaultAsync(p => p.cpf == login.cpf);
      return profile;
    }

    public async Task<bool> OrWhere(Profiles model)
    {
      var profile = await _dbSet.AsNoTracking().Where(p => (p.email == model.email) || (p.cpf == model.cpf)).CountAsync();
      return profile > 0;
    }
  }
}

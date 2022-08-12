using book_collection.Repositories;
using book_collection.Context;
using book_collection.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;

namespace book_collection.Repositories
{
  public class Repository<T> : IRepository<T> where T : BaseEntity 
  {
    protected AppDbContext _context;
    protected DbSet<T> _dbSet;

    public async Task<T> GetById(Guid id)
    {
      return await _dbSet.Where(x => x.id == id).SingleOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
      _dbSet.Add(entity);
      await _context.SaveChangesAsync();
      return entity;
    }

    public async Task UpdateAsync(Guid id, T entity)
    {
      var model = await GetById(id);
      _context.Entry(model).State = EntityState.Modified;
      _context.Update(entity);
      await _context.SaveChangesAsync();
      return;
    }

    public async Task DeleteAsync(Guid id)
    {
      var model = await GetById(id);
      if (model == null)
      {
        throw new Exception("user not found");
      }
      _context.Remove(model);
      await _context.SaveChangesAsync();
    }
  }
}


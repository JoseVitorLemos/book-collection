using System.Linq.Expressions;

namespace book_collection.Repositories
{
  public interface IRepository<T>
  {
    Task<T> GetById(Guid id);

    Task<T> CreateAsync(T entity);

    Task UpdateAsync(Guid id, T entity);

    Task DeleteAsync(Guid id);
  }
}

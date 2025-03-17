using ProvaPub.Domain.Models;
using System.Linq.Expressions;

namespace ProvaPub.Infrastructure.Repository
{
    public interface IRepositoryBase<T> where T : BaseModel
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> ListAll();
        T Add(T entity);
        Task<T> AddAsync(T entity);
        List<T> GetAll(Expression<Func<T, bool>> where);
        Task<int> CountAsync(Expression<Func<T, bool>> where);
        Task<T> FindAsync(Expression<Func<T, bool>> where);
    }
}

using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProvaPub.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
    {
        protected readonly TestDbContext _ctx;

        public RepositoryBase(TestDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public IQueryable<T> ListAll()
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public T Add(T entity)
        {
            _ctx.Set<T>().Add(entity);
            return entity;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            return entity;
        }

        public List<T> GetAll(Expression<Func<T, bool>> where)
        {
            return _ctx.Set<T>().Where(where).ToList(); 
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            return await _ctx.Set<T>().CountAsync(where);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> where)
        {
            return await _ctx.Set<T>().Where(where).FirstAsync();
        }
    }
}

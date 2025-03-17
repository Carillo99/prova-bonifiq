using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;

namespace ProvaPub.Infrastructure.Repository
{
    public class NumbersRepository : RepositoryBase<RandomNumber>, INumbersRepository
    {
        public NumbersRepository(TestDbContext ctx) : base(ctx) { }
    }
}

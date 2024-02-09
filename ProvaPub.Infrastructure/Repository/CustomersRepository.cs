using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;

namespace ProvaPub.Infrastructure.Repository
{
    public class CustomersRepository : RepositoryBase<Customer>, ICustomersRepository
    {
        public CustomersRepository(TestDbContext ctx) : base(ctx) { }
    }
}

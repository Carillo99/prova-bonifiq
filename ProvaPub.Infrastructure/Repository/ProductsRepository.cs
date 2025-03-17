using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;

namespace ProvaPub.Infrastructure.Repository
{
    public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository(TestDbContext ctx) : base(ctx) { }
    }
}

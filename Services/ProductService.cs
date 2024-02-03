using ProvaPub.API.Repository;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Models;

namespace ProvaPub.API.Services
{
	public class ProductService : IProductService
    {
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page)
		{
			return new ProductList() {  HasNext=false, TotalCount =10, Products = _ctx.Products.ToList() };
		}
	}
}

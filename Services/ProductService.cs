using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page, int rows)
		{
            return new ProductList() {  HasNext=false, TotalCount = rows, Products = _ctx.Products
				     .Skip(page * rows)
                     .Take(rows)
					 .ToList() };
		}

	}
}

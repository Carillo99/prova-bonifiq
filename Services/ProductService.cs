using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
        const int Rows = 10;
		TestDbContext _ctx;

		public ProductService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                .Options;

            _ctx = new TestDbContext(contextOptions);
		}

		public ProductList  ListProducts(int page)
        {
            var productList = new ProductList();

            try
			{
                productList.Products = _ctx.Products.Skip((page - 1) * Rows).ToList();
                productList.HasNext = productList.Products.Count > Rows;
                productList.Products = productList.Products.Take(Rows).ToList();
                productList.TotalCount = productList.Products.Count;
            }
			catch (Exception)
			{
				throw;
			}

			return productList;
        }
	}
}

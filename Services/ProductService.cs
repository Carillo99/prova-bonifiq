using ProvaPub.API.Repository;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;

namespace ProvaPub.API.Services
{
    public class ProductService : IProductService
    {
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductDTOList  ListProducts(int page, int rows)
		{
            return new ProductDTOList() {  HasNext=false, TotalCount = rows, Products = _ctx.Products
				     .Skip(page * rows)
                     .Take(rows)
					 .ToList() };
		}
	}
}

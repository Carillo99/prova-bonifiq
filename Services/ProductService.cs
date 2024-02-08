using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Infrastructure.Context;

namespace ProvaPub.API.Services
{
    public class ProductService : IProductService
    {
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductDTOList  ListProducts(FilterList filter)
		{
			var productDTOList = _ctx.Products
					 .Skip(filter.Page * filter.Rows)
					 .Take(filter.Rows + 1);

            return new ProductDTOList(productDTOList, filter.Rows);
		}
	}
}

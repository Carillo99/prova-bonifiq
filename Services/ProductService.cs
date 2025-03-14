using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public ProductDTOList ListProducts(FilterDTO filter)
        {
            var productList = new ProductDTOList();

            try
            {
                var productDTOList = _unitOfWork.ProductsRepository
                         .ListAll()
                         .Skip((filter.Page - 1) * filter.Rows);

                productList = new ProductDTOList(productDTOList, filter.Rows);
            }
			catch (Exception)
			{
				throw;
			}

			return productList;
        }
	}
}

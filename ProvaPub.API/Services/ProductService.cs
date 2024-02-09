using ProvaPub.Domain.DTO;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

		public ProductDTOList  ListProducts(FilterList filter)
        {
            if (filter?.Page < 0) throw new Exception("Número da página inválido.");
            if (filter?.Rows < 0) throw new Exception("Número de linhas inválido.");

            var productDTOList = _unitOfWork.ProductsRepository.ListAll()
                     .Skip(filter.Page * filter.Rows)
					 .Take(filter.Rows + 1);

            return new ProductDTOList(productDTOList, filter.Rows);
		}
	}
}

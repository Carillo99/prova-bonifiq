using ProvaPub.API.Validators;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Exceptions;
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

		public ProductDTOList  ListProducts(FilterDTO filter)
        {
            var validationResult = new FilterListValidator().Validate(filter);
            if (!validationResult.IsValid) throw new ValidationExceptionList(validationResult);

            var productDTOList = _unitOfWork.ProductsRepository.ListAll()
                     .Skip(filter.Page * filter.Rows)
					 .Take(filter.Rows + 1);

            return new ProductDTOList(productDTOList, filter.Rows);
		}
	}
}

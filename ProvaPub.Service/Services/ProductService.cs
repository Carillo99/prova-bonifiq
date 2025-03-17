using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Exceptions;
using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Validators;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Servise.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<string> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork
            , ILogger<string> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

		public async Task<OperationResponse<ProductDTOList>> ListProducts(FilterDTO filter)
        {
            var response = new OperationResponse<ProductDTOList>();

            try
            {
                var validationResult = new FilterListValidator().Validate(filter);
                if (!validationResult.IsValid) throw new ValidationExceptionList(validationResult);

                var productDTOList = _unitOfWork.ProductsRepository
                         .ListAll()
                         .Skip((filter.Page - 1) * filter.Rows);

                response.Data = new ProductDTOList(productDTOList, filter.Rows);
            }
			catch (Exception ex)
            {
                _logger.LogError($"ProductService - ListProducts - {ex.Message} - {JsonConvert.SerializeObject(filter)}");
                response.AddError(ex.Message, filter);
            }

			return response;
        }
	}
}

using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Infrastructure;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface IProductService
    {
        Task<OperationResponse<ProductDTOList>> ListProducts(FilterDTO filter);
    }
}

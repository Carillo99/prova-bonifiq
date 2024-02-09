using ProvaPub.Domain.DTO.Report;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface IProductService
    {
        ProductDTOList ListProducts(FilterDTO filter);
    }
}

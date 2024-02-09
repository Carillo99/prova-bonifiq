using ProvaPub.Domain.DTO;
using ProvaPub.Domain.DTO.Report;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface IProductService
    {
        ProductDTOList ListProducts(FilterList filter);
    }
}

using ProvaPub.Domain.DTO;
using ProvaPub.Domain.DTO.Report;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface ICustomerService
    {
        CustomerDTOList ListCustomers(FilterList filter);

        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}

using ProvaPub.Domain.DTO.Report;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface ICustomerService
    {
        CustomerDTOList ListCustomers(FilterDTO filter);

        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}

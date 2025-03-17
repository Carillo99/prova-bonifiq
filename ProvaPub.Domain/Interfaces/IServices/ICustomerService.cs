using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Infrastructure;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<OperationResponse<CustomerDTOList>> ListCustomers(FilterDTO filter);

        Task<bool> CanPurchase(int customerId, decimal purchaseValue, DateTime dateUtc);
    }
}

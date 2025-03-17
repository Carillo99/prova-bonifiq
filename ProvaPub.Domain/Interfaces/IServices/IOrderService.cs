using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Models;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<OperationResponse<Order>> PayOrder(OrderDTO mode);
    }
}

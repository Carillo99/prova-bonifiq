using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Enum;

namespace ProvaPub.Domain.Interfaces.IServices.IPayment.Base
{
    public interface IPaymentMethodService
    {
        public EnumPaymentMethod TypePaymentMethod { get; }
        Task<object> MakePayment(OrderDTO model);
    }
}

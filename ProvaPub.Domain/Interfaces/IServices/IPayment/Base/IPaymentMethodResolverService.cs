using ProvaPub.Domain.Enum;

namespace ProvaPub.Domain.Interfaces.IServices.IPayment.Base
{
    public interface IPaymentMethodResolverService
    {
        IPaymentMethodService Resolve(EnumPaymentMethod typePaymentMethod);
    }
}

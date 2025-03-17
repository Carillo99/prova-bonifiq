using ProvaPub.Domain.Enum;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;

namespace ProvaPub.Service.Services.Payment.Base
{
    public class PaymentMethodResolverService : IPaymentMethodResolverService
    {
        private readonly IEnumerable<IPaymentMethodService> _servicoPaymentMethods;

        public PaymentMethodResolverService(IEnumerable<IPaymentMethodService> servicoPaymentMethods)
        {
            _servicoPaymentMethods = servicoPaymentMethods;
        }

        public IPaymentMethodService Resolve(EnumPaymentMethod typePaymentMethod)
        {
            var paymentMethod = _servicoPaymentMethods.FirstOrDefault(item => item.TypePaymentMethod == typePaymentMethod);
            if (paymentMethod == null) throw new ArgumentException("Método de pagamento não encontrado");

            return paymentMethod;
        }
    }
}

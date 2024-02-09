using ProvaPub.Domain.Interfaces.IServices.IPayment;

namespace ProvaPub.API.Services.Payment
{
    public class PaymentStrategy : IPaymentStrategy
    {
        private IPayment _payment;

        public PaymentStrategy()
        { }

        public PaymentStrategy(IPayment payment)
        {
            this._payment = payment;
        }

        public void SetStrategy(IPayment payment)
        {
            this._payment = payment;
        }

        public void PaymentLogic()
        {
            _payment.MakePayment();
        }
    }
}

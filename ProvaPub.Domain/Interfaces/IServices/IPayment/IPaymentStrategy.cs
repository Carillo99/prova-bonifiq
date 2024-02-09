namespace ProvaPub.Domain.Interfaces.IServices.IPayment
{
    public interface IPaymentStrategy
    {
        void SetStrategy(IPayment payment);

        void PaymentLogic();
    }
}

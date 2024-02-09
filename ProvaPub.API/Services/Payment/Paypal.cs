using ProvaPub.Domain.Interfaces.IServices.IPayment;

namespace ProvaPub.API.Services.Payment
{
    public class Paypal : IPayment
    {
        public object MakePayment()
        {
            return true;
        }
    }
}

using ProvaPub.Domain.Interfaces.IServices.IPayment;

namespace ProvaPub.API.Services.Payment
{
    public class Creditcard : IPayment
    {
        public object MakePayment()
        {
            return true;
        }
    }
}

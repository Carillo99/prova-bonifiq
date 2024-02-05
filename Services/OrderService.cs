using ProvaPub.API.Services.Payment;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Interfaces.IServices.IPayment;
using ProvaPub.Domain.Models;

namespace ProvaPub.API.Services
{
	public class OrderService : IOrderService
    {
        private readonly IPaymentStrategy _paymentStrategy;

        public OrderService(IPaymentStrategy paymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
            switch (paymentMethod)
            {
                case "pix":
                    _paymentStrategy.SetStrategy( new Pix());
                    break;
                case "creditcard":
                    _paymentStrategy.SetStrategy(new Creditcard());
                    break;
                case "paypal":
                    _paymentStrategy.SetStrategy(new Paypal());
                    break;
                default:
                    throw new Exception("Tipo de pagamento não encontrado.");
            }

            _paymentStrategy.PaymentLogic();

            return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}

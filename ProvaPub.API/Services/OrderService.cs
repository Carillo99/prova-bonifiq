using ProvaPub.API.Services.Payment;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Interfaces.IServices.IPayment;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentStrategy _paymentStrategy;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IPaymentStrategy paymentStrategy, IUnitOfWork unitOfWork)
        {
            _paymentStrategy = paymentStrategy;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
            var customer = await _unitOfWork.CustomersRepository.GetByIdAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

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

            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Customer = customer
            });
		}
	}
}

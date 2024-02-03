using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Models;

namespace ProvaPub.API.Services
{
	public class OrderService : IOrderService
    {
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			if (paymentMethod == "pix")
			{
				//Faz pagamento...
			}
			else if (paymentMethod == "creditcard")
			{
				//Faz pagamento...
			}
			else if (paymentMethod == "paypal")
			{
				//Faz pagamento...
			}

			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}

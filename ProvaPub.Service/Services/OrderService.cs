using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Servise.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentMethodResolverService _servicoPaymentMethodResolver;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<string> _logger;

        public OrderService(IPaymentMethodResolverService servicoPaymentMethodResolver
            , IUnitOfWork unitOfWork
            , ILogger<string> logger)
        {
            _servicoPaymentMethodResolver = servicoPaymentMethodResolver;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OperationResponse<Order>> PayOrder(OrderDTO model)
        {
            var response = new OperationResponse<Order>();
            var order = new Order(model);

            try
            {
                response.Data = new Order(model); ;
                response.Data.Customer = await _unitOfWork.CustomersRepository.GetByIdAsync(order.CustomerId);
                if (response.Data.Customer == null) throw new InvalidOperationException($"Customer Id {order.CustomerId} does not exists");

                var paymentMethod = _servicoPaymentMethodResolver.Resolve(model.EPaymentMethod);
                await paymentMethod.MakePayment(model);
                await InsertOrder(order);

                response.Data.OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, TimeZoneInfo.FindSystemTimeZoneById("Brazil/East"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrderService - PayOrder - {ex.Message} - {JsonConvert.SerializeObject(model)}");
                response.AddError(ex.Message, model);
            }

            return await Task.FromResult(response);
        }

		public async Task<Order> InsertOrder(Order order)
        {
            //Insere pedido no banco de dados
            await _unitOfWork.OrdersRepository.AddAsync(order);
            _unitOfWork.Commit();

            return order;
        }
	}
}

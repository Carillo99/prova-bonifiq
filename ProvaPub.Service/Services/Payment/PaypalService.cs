using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Enum;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;

namespace ProvaPub.Servise.Services.Payment
{
    public class PaypalService : IPaymentMethodService
    {
        public EnumPaymentMethod TypePaymentMethod { get; }
        private readonly ILogger<string> _logger;

        public PaypalService(ILogger<string> logger)
        {
            TypePaymentMethod = EnumPaymentMethod.Paypal;
        }

        public async Task<object> MakePayment(OrderDTO model)
        {
            _logger.LogInformation($"PaypalService - MakePayment - {JsonConvert.SerializeObject(model)}");
            return true;
        }
    }
}

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Enum;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;

namespace ProvaPub.Servise.Services.Payment
{
    public class CreditcardService : IPaymentMethodService
    {
        public EnumPaymentMethod TypePaymentMethod { get; }
        private readonly ILogger<string> _logger;

        public CreditcardService(ILogger<string> logger)
        {
            TypePaymentMethod = EnumPaymentMethod.Creditcard;
        }

        public async Task<object> MakePayment(OrderDTO model)
        {
            _logger.LogInformation($"CreditcardService - MakePayment - {JsonConvert.SerializeObject(model)}");
            return true;
        }
    }
}

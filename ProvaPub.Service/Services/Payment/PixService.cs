using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO;
using ProvaPub.Domain.Enum;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;

namespace ProvaPub.Servise.Services.Payment
{
    public class PixService : IPaymentMethodService
    {
        public EnumPaymentMethod TypePaymentMethod { get; }
        private readonly ILogger<string> _logger;

        public PixService(ILogger<string> logger)
        {
            _logger = logger;
            TypePaymentMethod = EnumPaymentMethod.Pix;
        }

        public async Task<object> MakePayment(OrderDTO model)
        {
            _logger.LogInformation($"PixService - MakePayment - {JsonConvert.SerializeObject(model)}");
            return true;
        }
    }
}

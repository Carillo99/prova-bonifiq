using ProvaPub.Domain.Enum;

namespace ProvaPub.Domain.DTO
{
    public class OrderDTO
    {
        public EnumPaymentMethod EPaymentMethod { get; set; }
        public decimal PaymentValue { get; set; }
        public int CustomerId { get; set; }
    }
}

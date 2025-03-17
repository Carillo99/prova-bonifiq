using ProvaPub.Domain.DTO;
using System.Data;

namespace ProvaPub.Domain.Models
{
	public class Order : BaseModel
    {
        public Order()
        {
                
        }

        public Order(OrderDTO model)
        {
            CustomerId = model.CustomerId;
            Value = model.PaymentValue;
            OrderDate = DateTime.UtcNow;
        }

        public decimal Value { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public virtual Customer Customer { get; set; }
	}
}

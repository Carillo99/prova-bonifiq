namespace ProvaPub.Domain.Models
{
	public class Customer : BaseModel
    {
		public string Name { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}

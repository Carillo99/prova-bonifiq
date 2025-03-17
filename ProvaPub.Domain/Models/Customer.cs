namespace ProvaPub.Domain.Models
{
	public class Customer : BaseModel
    {
		public string Name { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}

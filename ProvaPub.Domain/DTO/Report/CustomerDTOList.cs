using ProvaPub.Domain.Models;

namespace ProvaPub.Domain.DTO.Report
{
    public class CustomerDTOList : BaseList
    {
        public CustomerDTOList(){}

        public CustomerDTOList(IQueryable<Customer> custumers, int rows) : base(custumers.Count(), rows)
        {
            Customers = custumers.Take(rows).ToList();
        }

        public List<Customer> Customers { get; set; }
    } 
}

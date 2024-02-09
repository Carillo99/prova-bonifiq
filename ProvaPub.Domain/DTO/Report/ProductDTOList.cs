using ProvaPub.Domain.Models;

namespace ProvaPub.Domain.DTO.Report
{
    public class ProductDTOList : BaseList
    {
        public ProductDTOList() { }
        public ProductDTOList(IQueryable<Product> products, int rows) : base(products.Count(), rows)
        {
            Products = products.Take(rows).ToList();
        }

        public List<Product> Products { get; set; }
    }
}

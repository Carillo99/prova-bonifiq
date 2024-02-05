using Microsoft.EntityFrameworkCore;
using ProvaPub.API.Repository;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Interfaces.IServices;

namespace ProvaPub.API.Services
{
    public class CustomerService : ICustomerService
    {
        TestDbContext _ctx;

        public CustomerService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public CustomerDTOList ListCustomers(FilterList filter)
        {
            var customerDTOList = _ctx.Customers
                     .Skip(filter.Page * filter.Rows)
                     .Take(filter.Rows + 1);

            return new CustomerDTOList(customerDTOList, filter.Rows);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }

    }
}

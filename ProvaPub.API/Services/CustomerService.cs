using Microsoft.EntityFrameworkCore;
using ProvaPub.API.Validators;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Exceptions;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Infrastructure.Context;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomerDTOList ListCustomers(FilterDTO filter)
        {
            var validationResult = new FilterListValidator().Validate(filter);
            if (!validationResult.IsValid) throw new ValidationExceptionList(validationResult);

            var customerDTOList = _unitOfWork.CustomersRepository.ListAll()
                     .Skip(filter.Page * filter.Rows)
                     .Take(filter.Rows + 1);

            return new CustomerDTOList(customerDTOList, filter.Rows);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            try
            {
                if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));
                if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

                //Business Rule: Non registered Customers cannot purchase
                var customer = _unitOfWork.CustomersRepository.GetByIdAsync(customerId);
                if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

                //Business Rule: A customer can purchase only a single time per month
                var baseDate = DateTime.UtcNow.AddMonths(-1);
                var ordersInThisMonth = await _unitOfWork.OrdersRepository.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
                if (ordersInThisMonth > 0)
                    return false;

                //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
                var haveBoughtBefore = await _unitOfWork.CustomersRepository.CountAsync(s => s.Id == customerId && s.Orders.Any());
                if (haveBoughtBefore == 0 && purchaseValue > 100)
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProvaPub.Domain.DTO.Report;
using ProvaPub.Domain.Exceptions;
using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Validators;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Servise.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<string> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork
            , ILogger<string> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OperationResponse<CustomerDTOList>> ListCustomers(FilterDTO filter)
        {
            var response = new OperationResponse<CustomerDTOList>();

            try
            {
                var validationResult = new FilterListValidator().Validate(filter);
                if (!validationResult.IsValid) throw new ValidationExceptionList(validationResult);

                var customerDTOList = _unitOfWork.CustomersRepository
                         .ListAll()
                         .Skip((filter.Page - 1) * filter.Rows);

                response.Data = new CustomerDTOList(customerDTOList, filter.Rows);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerService - ListCustomers - {ex.Message} - {JsonConvert.SerializeObject(filter)}");
                response.AddError(ex.Message, filter);
            }

            return response;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue, DateTime dateUtc)
        {
            try
            {
                if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

                if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

                //Business Rule: Non registered Customers cannot purchase
                var customer = await _unitOfWork.CustomersRepository.FindAsync(s => s.Id == customerId);
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

                //Business Rule: A customer can purchases only during business hours and working days
                if (dateUtc.Hour < 8 || dateUtc.Hour > 18 || dateUtc.DayOfWeek == DayOfWeek.Saturday || dateUtc.DayOfWeek == DayOfWeek.Sunday)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerService - CanPurchase - {ex.Message}");
            }


            return false;
        }

    }
}

using Bogus.DataSets;
using Microsoft.Extensions.Logging;
using Moq;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Repository;
using ProvaPub.Servise.Services;
using System.Linq.Expressions;

namespace ProvaPub.Application.UnitTest
{
    public class CustomerServiceUnitTest
    {
        private readonly  Mock<ILogger<string>> _mockLogger;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private CustomerService _customerService;
        public CustomerServiceUnitTest()
        {
            _mockLogger = new Mock<ILogger<string>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _customerService = new CustomerService(_mockUnitOfWork.Object, _mockLogger.Object);
        }

        [Theory]
        [InlineData(-1, 0)]
        public void CustomerInvalido(int customerId, decimal purchaseValue)
        {
            // Act
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(1, -1)]
        public void PurchaseValueInvalido(int customerId, decimal purchaseValue)
        {
            // Act
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 20)]
        public void NonRegisteredCustomers(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => null);

            // Act
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(1, 20)]
        public void CustomerCanPurchaseOnlySingleTimePerMonth(int customerId, decimal purchaseValue)
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderDate = new DateTime(2025, 03, 12), Value = 10, CustomerId = 1 }
            };

            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = orders }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 1);

            // Act
            var result = _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow).Result;

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, 120)]
        public void CustomerThatNeverBoughtBeforeCanMakeAFirstPurchaseOfMaximum(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            // Act
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, DateTime.UtcNow).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 20)]
        public void CustomerCanPurchasesOnlyDuringBusinessHours(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            // Act
            var dateUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 03, 13, 20, 50, 2));
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, dateUtc).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 20)]
        public void CustomerCanPurchasesOnlyDuringBusinessworkingDays(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            // Act
            var dateUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 03, 9)); 
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, dateUtc).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 120)]
        public void CustomerCanPurchasesOnlyDuringBusinessHours2(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => 3);

            // Act
            var dateUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 03, 13, 20, 50, 2));
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, dateUtc).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 120)]
        public void CustomerCanPurchasesOnlyDuringBusinessworkingDays2(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.CountAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => 3);

            // Act
            var dateUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 03, 9));
            var canPuchase = _customerService.CanPurchase(40, 120, dateUtc).Result;

            // Assert
            Assert.False(canPuchase);
        }

        [Theory]
        [InlineData(40, 20)]
        public void SucessCase(int customerId, decimal purchaseValue)
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Roberto", Orders = new List<Order>() }
            };

            _mockUnitOfWork.Setup(repo => repo.CustomersRepository.FindAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Expression<Func<Customer, bool>> predicate) => customers.Find(m => m.Id == 1));

            _mockUnitOfWork.Setup(repo => repo.OrdersRepository.CountAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Expression<Func<Order, bool>> predicate) => 0);

            // Act
            var dateUtc = TimeZoneInfo.ConvertTimeToUtc(new DateTime(2025, 03, 14, 10, 50, 2));
            var canPuchase = _customerService.CanPurchase(customerId, purchaseValue, dateUtc).Result;

            // Assert
            Assert.True(canPuchase);
        }
    }
}
using ProvaPub.Api.IntegrationTests.Base;
using ProvaPub.API.Services;
using ProvaPub.Domain.Interfaces.IServices;

namespace ProvaPub.Application.UnitTests.ServiceTest
{
    public class CanPurchaseTest : TestBase
    {
        private readonly ICustomerService _customer; 
        private const int CustomerId_INVALID = -1;
        private const int CustomerId_NOT_FOUND = 35;
        private const int CustomerId_FOUND = 2;

        private const int PurchaseValue_INVALID = 200;
        private const int PurchaseValue_VALID = 50;

        public CanPurchaseTest()
        {
            _customer = new CustomerService(_unitOfWork);
        }

        [Fact]
        public void CustomerId_PurchaseValue_INVALID()
        {
            var puchase =  _customer.CanPurchase(CustomerId_INVALID, PurchaseValue_INVALID).Result;
            Assert.False(puchase);
        }
        [Fact]
        public void CustomerId_INVALID_PurchaseValue_VALID()
        {
            var puchase = _customer.CanPurchase(CustomerId_INVALID, PurchaseValue_VALID).Result;
            Assert.False(puchase);
        }

        [Fact]
        public void CustomerId_Not_Found_PurchaseValue_INVALID()
        {
            var puchase = _customer.CanPurchase(CustomerId_NOT_FOUND, PurchaseValue_INVALID).Result;
            Assert.False(puchase);
        }
        [Fact]
        public void CustomerId_Not_Found_PurchaseValue_VALID()
        {
            var puchase = _customer.CanPurchase(CustomerId_NOT_FOUND, PurchaseValue_INVALID).Result;
            Assert.False(puchase);
        }
        [Fact]
        public void CustomerId_FOUND_INVALID_PurchaseValue_VALID()
        {
            var puchase = _customer.CanPurchase(CustomerId_FOUND, PurchaseValue_VALID).Result;
            Assert.True(puchase);
        }

        [Fact]
        public void CustomerId_FOUND_PurchaseValue_INVALID()
        {
            var puchase = _customer.CanPurchase(CustomerId_FOUND, PurchaseValue_INVALID).Result;
            Assert.False(puchase);
        }
    }
}
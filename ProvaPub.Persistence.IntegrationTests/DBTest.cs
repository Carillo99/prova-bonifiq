using ProvaPub.Api.IntegrationTests.Base;

namespace ProvaPub.Persistence.IntegrationTests
{
    public class DBTest : TestBase
    {
        [Fact]
        public void GetCustomer()
        {
            int customerId = 1;

            var customer = _unitOfWork.CustomersRepository.GetByIdAsync(customerId).Result; 

            Assert.NotNull(customer);
        }
    }
}
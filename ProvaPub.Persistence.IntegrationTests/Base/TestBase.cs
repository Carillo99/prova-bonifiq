using Microsoft.EntityFrameworkCore;
using ProvaPub.Infrastructure.Context;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Api.IntegrationTests.Base
{
    public abstract class TestBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        private bool _databaseInitialized = false;

        public TestBase()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "Teste")
            .Options;

            var ctx = new TestDbContext(options);
            if (!_databaseInitialized)
            {

                Utilities.InitializeDbForTests(ctx);
                _databaseInitialized = true;
            }

            _unitOfWork = new UnitOfWork(ctx);
        }
    }
}

using ProvaPub.Infrastructure.Context;

namespace ProvaPub.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestDbContext _ctx;

        public INumbersRepository NumbersRepository { get; }
        public ICustomersRepository CustomersRepository { get; }
        public IOrdersRepository OrdersRepository { get; }
        public IProductsRepository ProductsRepository { get; }

        public UnitOfWork(TestDbContext ctx)
        {
            _ctx = ctx;
            NumbersRepository = new NumbersRepository(_ctx);
            CustomersRepository = new CustomersRepository(_ctx);
            OrdersRepository = new OrdersRepository(_ctx);
            ProductsRepository = new ProductsRepository(_ctx);
        }

        public void Commit()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

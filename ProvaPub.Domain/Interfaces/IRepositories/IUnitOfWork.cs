namespace ProvaPub.Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomersRepository CustomersRepository { get; }

        IOrdersRepository OrdersRepository { get; }

        IProductsRepository ProductsRepository { get; }
        void Commit();
    }
}

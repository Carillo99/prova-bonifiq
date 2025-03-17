namespace ProvaPub.Infrastructure.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        INumbersRepository NumbersRepository { get; }
        ICustomersRepository CustomersRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IProductsRepository ProductsRepository { get; }
        void Commit();
    }
}

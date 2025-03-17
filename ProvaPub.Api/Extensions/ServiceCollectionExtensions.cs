using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;
using ProvaPub.Infrastructure.Context;
using ProvaPub.Infrastructure.Repository;
using ProvaPub.Service.Services.Payment.Base;
using ProvaPub.Servise.Services;
using ProvaPub.Servise.Services.Payment;

namespace ProvaPub.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ctx")));

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IRandomService, RandomService>();
            services.AddScoped<IPaymentMethodResolverService, PaymentMethodResolverService>();
            services.AddScoped<IPaymentMethodService, CreditcardService>();
            services.AddScoped<IPaymentMethodService, PaypalService>();
            services.AddScoped<IPaymentMethodService, PixService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }
    }
}
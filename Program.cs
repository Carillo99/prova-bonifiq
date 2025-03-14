using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Interfaces.IServices.IPayment.Base;
using ProvaPub.Infrastructure.Context;
using ProvaPub.Infrastructure.Repository;
using ProvaPub.Service.Services.Payment.Base;
using ProvaPub.Servise.Services;
using ProvaPub.Servise.Services.Payment;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddSingleton<RandomService>();
builder.Services.AddScoped<IPaymentMethodResolverService, PaymentMethodResolverService>();
builder.Services.AddScoped<IPaymentMethodService, CreditcardService>();
builder.Services.AddScoped<IPaymentMethodService, PaypalService>();
builder.Services.AddScoped<IPaymentMethodService, PixService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

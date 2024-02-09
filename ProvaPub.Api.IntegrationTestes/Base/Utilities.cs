using Bogus;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;
using System;

namespace ProvaPub.Api.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(TestDbContext ctx)
        {
            for (int i = 0; i < 20; i++)
            {
                ctx.Customers.Add(new Customer()
                {
                    Id = i + 1,
                    Name = new Faker().Person.FullName,
                });
            }

            ctx.SaveChanges();
        }
    }
}

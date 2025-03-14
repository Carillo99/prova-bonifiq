﻿using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Context;

namespace ProvaPub.Api.Services
{
	public class RandomService
	{
		int seed;
        TestDbContext _ctx;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
    .Options;
            seed = Guid.NewGuid().GetHashCode();

            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
		{
            var number =  new Random(seed).Next(100);
            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            _ctx.SaveChanges();
			return number;
		}

	}
}

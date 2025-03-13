﻿using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
        TestDbContext _ctx;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                .Options;

            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
		{
			try
			{
                var seed = Guid.NewGuid().GetHashCode();
                var number = new Random(seed).Next(100);
                _ctx.Numbers.Add(new RandomNumber() { Number = number });
                _ctx.SaveChanges();

                return number;
            }
			catch (Exception)
			{
                return -1;
			}
		}
	}
}

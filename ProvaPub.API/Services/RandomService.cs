using ProvaPub.Domain.Interfaces.IServices;

namespace ProvaPub.API.Services
{
	public class RandomService : IRandomService
    {
		public RandomService()
		{
		}

		public int GetRandom()
		{
			return new Random().Next(100);
		}
	}
}

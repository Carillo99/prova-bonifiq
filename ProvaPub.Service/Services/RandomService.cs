using Microsoft.Extensions.Logging;
using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Interfaces.IServices;
using ProvaPub.Domain.Models;
using ProvaPub.Infrastructure.Repository;

namespace ProvaPub.Servise.Services
{
    public class RandomService : IRandomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<string> _logger;

        public RandomService(IUnitOfWork unitOfWork
            , ILogger<string> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<OperationResponse<RandomNumber>> GetRandom()
        {
            var response = new OperationResponse<RandomNumber>();

            try
			{
                var seed = Guid.NewGuid().GetHashCode();
                response.Data.Number = new Random(seed).Next(100);
                await _unitOfWork.NumbersRepository.AddAsync(response.Data);
                _unitOfWork.Commit();
            }
			catch (Exception ex)
            {
                _logger.LogError($"ProductService - ListProducts - {ex.Message}");
                response.AddError(ex.Message, null);
                response.Data.Number = - 1;
            }

            return response;
        }
	}
}

using ProvaPub.Domain.Infrastructure;
using ProvaPub.Domain.Models;

namespace ProvaPub.Domain.Interfaces.IServices
{
    public interface IRandomService
    {
        Task<OperationResponse<RandomNumber>> GetRandom();
    }
}

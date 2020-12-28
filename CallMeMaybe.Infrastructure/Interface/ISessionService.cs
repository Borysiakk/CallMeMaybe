using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface ISessionService
    {
        Task AddAsync(Session session);
        Task<List<string>> GetActiveUserAsync(string userId);
        Task<List<string>> GetConnectionsIdByUser(string userId);
    }
}
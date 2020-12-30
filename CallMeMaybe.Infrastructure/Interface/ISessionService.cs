using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface ISessionService
    {
        Task AddAsync(Session session);
        Task<string> GetUserIdByConnectionId(string connectionId);
        Task<string> GetUserNameByConnectionId(string connectionId);
        Task<string> GetConnectionIdByUserName(string userName);
        Task UpdateStatusAsync(string connectionId, bool status);
    }
}
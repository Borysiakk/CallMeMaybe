using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface ISessionService
    {
        public Task AddAsync(SessionUser session);
        public void CloseSessionUser(string userName);
        public string GetConnectionIdByUserId(string userId);
        public string GetConnectionIdByUserName(string userName);
        public string GetUserNameByConnectionId(string connectionId);
        public string GetUserIdByConnectionId(string connectedId);
        
    }
}
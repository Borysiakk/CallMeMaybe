using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;

namespace CallMeMaybe.Infrastructure.Services
{
    public class SessionService :ISessionService
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(SessionUser session)
        {
            await _dbContext.SessionUsers.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }

        public void CloseSessionUser(string userName)
        {
            var session = _dbContext.SessionUsers.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.UserName == userName);
            if (session != null)
            {
                session.StatusConnection = false;
                _dbContext.SessionUsers.Update(session);
            }
            _dbContext.SaveChanges();
        }

        public string GetConnectionIdByUserId(string userId)
        {
            return _dbContext.SessionUsers.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.UserId == userId)?.ConnectionId;
        }

        public string GetConnectionIdByUserName(string userName)
        {
            return _dbContext.SessionUsers.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.UserName == userName)?.ConnectionId;
        }

        public string GetUserNameByConnectionId(string connectionId)
        {
            return _dbContext.SessionUsers.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.ConnectionId == connectionId)?.UserName;
        }

        public string GetUserIdByConnectionId(string connectedId)
        {
           return _dbContext.SessionUsers.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.ConnectionId == connectedId)?.UserId;
        }
    }
}
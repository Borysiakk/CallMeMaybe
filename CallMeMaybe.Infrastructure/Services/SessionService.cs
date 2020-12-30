

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CallMeMaybe.Infrastructure.Services
{
    public class SessionService :ISessionService
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Session session)
        {
            await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }

        public Task<string> GetUserIdByConnectionId(string connectionId)
        {
           return _dbContext.Sessions.Where(a => a.Status == true).Where(b => b.ConnectionId == connectionId).Select(c => c.UserId).FirstOrDefaultAsync();
        }

        public async Task<string> GetUserNameByConnectionId(string connectionId)
        {
            var id = await GetUserIdByConnectionId(connectionId);
            return await _dbContext.Users.Where(a => a.Id == id).Select(b => b.UserName).FirstOrDefaultAsync();
        }

        public async Task<string> GetConnectionIdByUserName(string userName)
        {
            var r =   await (from user in _dbContext.Users
                    where user.UserName == userName
                    join session in _dbContext.Sessions on user.Id equals session.UserId
                    select session).Where(a=>a.Status == true).Select(b=>b.ConnectionId).FirstOrDefaultAsync();
            return r;
        }

        public async Task UpdateStatusAsync(string connectionId, bool status)
        {
            var session = await _dbContext.Sessions.Where(a => a.Status == true).SingleOrDefaultAsync(b => b.ConnectionId == connectionId);
            session.Status = status;
            _dbContext.Sessions.Update(session);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
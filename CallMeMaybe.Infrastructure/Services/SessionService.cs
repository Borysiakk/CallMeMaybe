

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

        public async Task<List<string>> GetActiveUserAsync(string userId)
        {
            return await _dbContext.Sessions.Where(a => a.Status == true).Where(b => b.UserId == userId).Select(c => c.UserId).ToListAsync();
        }

        public async Task<List<string>> GetConnectionsIdByUser(string userId)
        {
            return await _dbContext.Sessions.Where(a => a.UserId == userId).Where(b => b.Status == true).Select(c => c.ConnectionId).ToListAsync();
        }
    }
}
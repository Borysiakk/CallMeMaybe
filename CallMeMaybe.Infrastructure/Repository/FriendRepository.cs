using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CallMeMaybe.Infrastructure.Repository
{
    public class FriendRepository :IFriendRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetFriendsAsync(string userId)
        {
            return await (from friend in _dbContext.Friends join user in _dbContext.Users on friend.FriendId equals user.Id where friend.UserId == userId select user.UserName).ToListAsync();
        }

        public async Task<List<string>> GetActiveFriends(string userId)
        {
            return await (from friend in _dbContext.Friends join user in _dbContext.Users on friend.FriendId equals user.Id select user.UserName).Distinct().ToListAsync();
        }
    }
}
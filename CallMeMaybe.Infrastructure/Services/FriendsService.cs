using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CallMeMaybe.Infrastructure.Services
{
    public class FriendsService :IFriendsService
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> GetUserNameActiveFriend(string userId)
        {
            return _dbContext.Friends.Where(a => a.UserId == userId).Join(_dbContext.SessionUsers.Where(x=>x.StatusConnection == true), a => a.FriendId,
            b => b.UserId, (a, b) => new string(b.UserName)).ToList();
        }

        public IEnumerable<string> GetConnectionIdActiveFriends(string userId)
        {
            return _dbContext.Friends.Where(a => a.UserId == userId).Join(_dbContext.SessionUsers.Where(x=>x.StatusConnection == true), a => a.FriendId,
            b => b.UserId, (a, b) => new string(b.ConnectionId)).ToList();
        }

        public IEnumerable<string> GetActiveFriendByUserName(string userName)
        {
            var id = _dbContext.Users.FirstOrDefault(a => a.UserName == userName)?.Id;
            return _dbContext.Friends.Where(a => a.UserId == id).Join(_dbContext.SessionUsers.Where(x=>x.StatusConnection == true), a => a.FriendId,
                    b => b.UserId, (a, b) => new string(b.ConnectionId)).ToList();
        }

        public async Task<Dictionary<string, bool>> GetFriendsWithStatusAsync(string userId)
        {
            Dictionary<string, bool> friendsStatus = new Dictionary<string, bool>();
            var friendsActive = GetUserNameActiveFriend(userId);
            var friends = await _dbContext.Friends.Where(a => a.UserId == userId).ToListAsync();
            foreach (var friend in friends)
            {
                friendsStatus.Add(friend.FriendName,false);
            }

            foreach (var friendActive in friendsActive)
            {
                if (friendsStatus.ContainsKey(friendActive))
                {
                    friendsStatus[friendActive] = true;
                }
            }
            return friendsStatus;
        }
    }
}
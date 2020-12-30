using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CallMeMaybe.Infrastructure.Services
{
    public class FriendService :IFriendService
    {
        private readonly ApplicationDbContext _dbContext;

        public FriendService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<ApplicationUser>> GetFriendsAsync(string userId)
        {
            return await (from friend in _dbContext.Friends
                join user in _dbContext.Users on friend.FriendId equals user.Id
                where friend.UserId == userId
                select user).ToListAsync();
        }

        public async Task<List<string>> GetFriendsByUserAsync(string userId)
        {
            return (await GetFriendsAsync(userId)).Select(a => a.UserName).ToList();
        }

        public async Task<List<string>> GetFriendByIdAsync(string userId)
        {
            return (await GetFriendsAsync(userId)).Select(a => a.Id).ToList();
        }

        public async Task<List<string>> GetActiveFriendsAsync(string userId)
        {
            return await (from session in _dbContext.Sessions where session.Status == true where session.UserId != userId
                join friends in _dbContext.Friends on session.UserId equals friends.FriendId
                join user in _dbContext.Users on session.UserId equals user.Id
                select user.UserName).Distinct().ToListAsync();
        }

        public async Task<Dictionary<string, bool>> GetFriendsStatusAsync(string userId)
        {
            Dictionary<string, bool> friendsStatus = new Dictionary<string, bool>();
            var friends = await GetFriendsByUserAsync(userId);
            var friendsActive = await GetActiveFriendsAsync(userId);
            
            foreach (var friend in friends)
            {
                friendsStatus.Add(friend,false);
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

        public async Task<List<string>> GetConnectionIdActiveFriendsAsync(string userId)
        {
            return await (from friends in _dbContext.Friends
                where friends.UserId == userId
                join session in _dbContext.Sessions on friends.FriendId equals session.UserId
                where session.Status == true
                select session.ConnectionId).ToListAsync();
        }
    }
}
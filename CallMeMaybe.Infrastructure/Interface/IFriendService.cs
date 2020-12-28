using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IFriendService
    {
        public Task<List<string>> GetFriendsAsync(string userId);
        public Task<List<string>> GetActiveFriends(string userId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IFriendService
    {
        public Task<List<ApplicationUser>> GetFriendsAsync(string userId);
        public Task<List<string>> GetFriendsByUserAsync(string userId);
        public Task<List<string>> GetFriendByIdAsync(string userId);
        public Task<List<string>> GetActiveFriendsAsync(string userId);
        public Task<Dictionary<string, bool>> GetFriendsStatusAsync(string userId);
        public Task<List<string>> GetConnectionIdActiveFriendsAsync(string userId);
    }
}
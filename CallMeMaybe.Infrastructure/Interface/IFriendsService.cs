using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IFriendsService
    {
        IEnumerable<string> GetUserNameActiveFriend(string userId);
        IEnumerable<string> GetConnectionIdActiveFriends(string userId);
        IEnumerable<string> GetActiveFriendByUserName(string userName);
        Task<Dictionary<string, bool>> GetFriendsWithStatusAsync(string userId);
    }
}
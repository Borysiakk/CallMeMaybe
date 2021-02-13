using System.Threading.Tasks;
using CallMeMaybe.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CallMeMaybe.Web.Controllers.Api
{
    [ApiController]
    [Route("api/Friends")]
    public class FriendsController:ControllerBase
    {
        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet("GetConnectionIdActiveFriends/{userId}")]
        public IActionResult GetConnectionIdActiveFriends(string userId)
        {
            var connectionsId = _friendsService.GetConnectionIdActiveFriends(userId);
            return Ok(connectionsId);
        }
        
        [HttpGet("GetFriendsWithStatus/{userId}")]
        public async Task<IActionResult> GetFriendsWithStatusAsync(string userId)
        {
            var friends = await _friendsService.GetFriendsWithStatusAsync(userId);
            return Ok(friends);
        }
    }
}
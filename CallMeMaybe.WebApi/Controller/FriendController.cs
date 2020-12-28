using System;
using System.Threading.Tasks;
using CallMeMaybe.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallMeMaybe.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendController :ControllerBase
    {
        private readonly IFriendService _friendRepository;
        
        public FriendController(IFriendService friendRepository)
        {
            _friendRepository = friendRepository;
        }
        
        [HttpGet("GetFriends/{userId}")]
        public async Task<IActionResult> GetFriends(string userId)
        {
            try
            {
                return Ok(await _friendRepository.GetFriendsAsync(userId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpGet("GetActiveFriends/{userId}")]
        public async Task<IActionResult> GetActiveFriends(string userId)
        {
            try
            {
                return Ok(await _friendRepository.GetActiveFriends(userId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }
    }
}
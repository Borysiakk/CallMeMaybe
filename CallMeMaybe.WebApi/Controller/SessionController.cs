using System;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallMeMaybe.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = _sessionService;
        }
    
        [HttpPost]
        public async Task<IActionResult> Add(Session session)
        {
            try
            {
                await _sessionService.AddAsync(session);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }
    }   
}
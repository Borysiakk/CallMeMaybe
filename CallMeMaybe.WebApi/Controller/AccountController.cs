using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Infrastructure.Interface;

namespace CallMeMaybe.WebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController :ControllerBase
    {
        private IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _identityService.LoginAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }

                return new BadRequestObjectResult(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
            
        }
    }
}
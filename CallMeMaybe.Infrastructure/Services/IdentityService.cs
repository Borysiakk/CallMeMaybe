using System.Linq;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Results;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;

namespace CallMeMaybe.Infrastructure.Services
{
    public class IdentityService :IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(ITokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticateResult> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                return new AuthenticateResult()
                {
                    Id = user.Id,
                    User = model.Email,
                    Success = true,
                    Token = _tokenService.Generate(user),
                };
            }
            
            return new AuthenticateResult()
            {
                Success = false,
                Errors = new string[] {"Błędny login lub hasło"}
            };
            
        }

        public async Task<AuthenticateResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser() {Email = model.Email, UserName = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new AuthenticateResult()
                {
                    Id = user.Id,
                    Success = true,
                    Token = _tokenService.Generate(user),
                };
            }
            
            return new AuthenticateResult()
            {
                Success = false,
                Errors = result.Errors.Select(a => a.Description).ToArray(),
            };
        }
    }
}
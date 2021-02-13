using System;
using System.Linq;
using System.Net;
using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CallMeMaybe.Infrastructure.Services
{
    public class IdentityService :IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<HttpAuthorizationResult> LoginAsync(LoginModelView loginModelView)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModelView.Email, loginModelView.Password, loginModelView.RememberMe,false);
            if (!result.Succeeded)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Login lub hasło są nie poprawne"},
                };
            }

            var user = await _userManager.FindByEmailAsync(loginModelView.Email);
            return new HttpAuthorizationResult()
            {
                Id = user.Id,
                User = user.Email,
                DateIssue = DateTime.Now,
                Code = HttpStatusCode.OK,
                Token = _tokenService.Generate(user),
            };
        }

        public async Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var user = new ApplicationUser() 
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            
            var isUserExist = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (isUserExist != null)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Znaleziono użytkownika o podanym mailu"},
                };
            }

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(a => a.Description).ToList(),
                };
            }
            return new HttpAuthorizationResult()
            {
                Id = user.Id,
                User = user.Email,
                Code = HttpStatusCode.OK,
                DateIssue = DateTime.Now,
                Token = _tokenService.Generate(user),
            };

        }
    }
}
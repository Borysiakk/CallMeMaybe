using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;
using CallMeMaybe.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IIdentityService
    {
        public Task<HttpAuthorizationResult> LoginAsync(LoginModelView loginModelView);
        public Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}
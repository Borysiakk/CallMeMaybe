using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IAccountService
    {
        public Task<HttpAuthorizationResult> LoginAsync(LoginModelView loginModelView);
        public Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}
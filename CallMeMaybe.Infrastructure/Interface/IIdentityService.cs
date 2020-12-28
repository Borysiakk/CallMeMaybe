using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Results;

namespace CallMeMaybe.Infrastructure.Interface
{
    public interface IIdentityService
    {
        Task LogoutAsync();
        Task<AuthenticateResult> LoginAsync(LoginViewModel model);
        Task<AuthenticateResult> RegisterAsync(RegisterViewModel model);
    }
}
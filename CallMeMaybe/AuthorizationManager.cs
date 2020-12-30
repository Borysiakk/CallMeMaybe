using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Results;

namespace CallMeMaybe
{
    public class AuthorizationManager
    {
        private LoginViewModel _login;
        public AuthenticateResult AuthenticateResult;

        public AuthorizationManager(LoginViewModel login)
        {
            _login = login;
        }

        public async Task<AuthenticateResult> RefreshToken()
        {
            AuthenticateResult = await HttpRestClient.LoginAsync(_login);
            return AuthenticateResult;
        }
    }
}
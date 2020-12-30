
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Results;
using CallMeMaybe.RestApi;


namespace CallMeMaybe
{
    public static class HttpRestClient
    {
        public static async Task<AuthenticateResult> LoginAsync(LoginViewModel model)
        {
            return await HttpHelper.Post<AuthenticateResult, LoginViewModel>(Routes.Account.Login,model);
        }

        public static async Task<List<string>> GetFriends(string userId)
        {
            return await HttpHelper.GetStringAsync<List<string>>(Routes.Friends.GetFriends,"userId", userId);
        }

        public static async Task<List<string>> GetActiveFriends(string userId)
        {
            return await HttpHelper.GetStringAsync<List<string>>(Routes.Friends.GetActiveFriends,"userId", userId);
        }

        public static async Task<Dictionary<string, bool>> GetFriendsStatus(string userId)
        {
            return await HttpHelper.GetStringAsync<Dictionary<string, bool>>(Routes.Friends.GetActiveStatus,"userId", userId);
        }
    }
}
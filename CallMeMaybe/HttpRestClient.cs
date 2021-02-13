using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;
using CallMeMaybe.Http;

namespace CallMeMaybe
{
    public class HttpRestClient
    {
        public static async Task<HttpAuthorizationResult> LoginAsync(LoginModelView model)
        {
            return await HttpHelper.Post<HttpAuthorizationResult, LoginModelView>(Routes.Identity.Login,model);
        }

        public static async Task<Dictionary<string, bool>> GetFriendsWithStatus(string userId)
        {
            return await HttpHelper.GetStringAsync<Dictionary<string, bool>>(Routes.Friends.GetFriendsWithStatus, "userId", userId);
        }
    }
}
using System;
using System.Collections.Generic;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Results;
using CallMeMaybe.RestApi;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;

namespace CallMeMaybe
{
    public static class HttpRestClient
    {
        public static AuthenticateResult Login(LoginViewModel model)
        {
            return HttpHelper.Post<AuthenticateResult, LoginViewModel>(Routes.Account.Login,model);
        }

        public static List<string> GetFriends(string userId)
        {
            return HttpHelper.GetString<List<string>, string>(Routes.Friends.GetFriends, userId);
        }

        public static List<string> GetActiveFriends(string userId)
        {
            return HttpHelper.GetString<List<string>, string>(Routes.Friends.GetActiveFriends, userId);
        }
    }
}
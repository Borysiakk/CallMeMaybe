using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe
{
    public static class Routes
    {
        public static class Account
        {
            public static string Login = "/api/Account/Login";
        }
        public static class Friends
        {
            public static string GetFriends = "/api/Friend/GetFriends/{userId}";
            public static string GetActiveFriends = "/api/Friend/GetActiveFriends/{userId}";
        }
    }
}
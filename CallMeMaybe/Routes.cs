using CallMeMaybe.Domain.Entities;

namespace CallMeMaybe
{
    public static class Routes
    {
        public static string Root = "https://localhost:5001";
        public static class Account
        {
            public static string Login = "/api/Account/Login";
        }
        public static class Friends
        {
            public static string GetFriends = "/api/Friend/GetFriends/{userId}";
            public static string GetActiveFriends = "/api/Friend/GetActiveFriends/{userId}";
            public static string GetActiveStatus = "/api/Friend/GetFriendsStatus/{userId}";
        }

        public static class SignalR
        {
            public static string Connection = Root + "/CallMeMaybeHub";
        }
    }
}
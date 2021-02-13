namespace CallMeMaybe
{
    public static class Routes
    {
        public static string Root = "https://localhost:5001";
        public static class Identity
        {
            public static string Login = "/api/Identity/Login";
        }
        public static class Friends
        {
            public static string GetFriendsWithStatus = "/api/Friends/GetFriendsWithStatus/{userId}";
            public static string GetConnectionIdActiveFriends = "/api/Friends/GetConnectionIdActiveFriends/{userId}";
        }
        public static class SignalR
        {
            public static string Connection = Root + "/CommunicationServerHubs";
        }
    }
}
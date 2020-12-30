
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

using CallMeMaybe.Args;

namespace CallMeMaybe.Builder
{
    public static class ConnectionBuilder
    {
        public static async Task<Connection> Create(ConnectionBuilderConfiguration builderConfiguration)
        {
            var authorizationManager = builderConfiguration.AuthorizationManager;
            string id = authorizationManager.AuthenticateResult.Id;
            string userName = authorizationManager.AuthenticateResult.User;

            HubConnection hub = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(authorizationManager.AuthenticateResult.Token);
                options.Headers.Add("Id", id);
                options.Headers.Add("UserName", userName);
            }).AddMessagePackProtocol().Build();

            Connection connection = new Connection(new ConnectionConfiguration
            {
                HubConnection = hub,
            });

            connection.SendMessage += builderConfiguration.SendMessageDelegate;
            connection.ReceiveMessage += builderConfiguration.ReceiveMessageDelegate;
            connection.UpdateFriendsStatus += builderConfiguration.UpdateFriendsStatusDelegate;


            connection.Friends = await HttpRestClient.GetFriendsStatus(id);

            hub.On("NotificationFriendStatus", (string userName, bool state) =>
            {
                try
                {
                    connection.Friends[userName] = state;
                    var args = new UpdateFriendsStatusDelegateArgs()
                    {
                        Status = state,
                        UserName = userName,
                    };

                    connection.OnUpdateFriendsStatus(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            });

            hub.On("BroadcastMessage", (string username,string message) =>
            {
                
                try
                {
                    var args = new MessageDelegateArgs()
                    {
                        UserName = username,
                        Message = message,
                    };
                
                    connection.OnReceiveMessage(args);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });


            await hub.StartAsync();

            connection.HubConnection = hub;
            connection.AuthorizationManager = authorizationManager;
            return connection;
        }
    }
}

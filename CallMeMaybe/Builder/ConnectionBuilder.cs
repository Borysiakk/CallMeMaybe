
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
        public static async Task<Connection> Create(AuthorizationManager authorizationManager)
        {
            string id = authorizationManager.AuthenticateResult.Id;
            string userName = authorizationManager.AuthenticateResult.User;

            HubConnection hub = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(authorizationManager.AuthenticateResult.Token);
                options.Headers.Add("Id", id);
                options.Headers.Add("UserName", userName);
            }).AddMessagePackProtocol().Build();

            Connection connection = new Connection()
            {
                HubConnection = hub
            };
            
            connection.Friends = await HttpRestClient.GetFriendsStatus(id);

            hub.On("FriendStatusNotification", (string userName, bool state) =>
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
                var args = new MessageDelegateArgs()
                {
                    UserName = username,
                    Message = message,
                };
            
                connection.OnReceiveMessage(args);
            });
            
            hub.On("IncomingCall", (string userName )=>
            {
                var args = new CommunicatorDelegateArgs()
                {
                    UserName = userName,
                };
                
                connection.OnIncomingCall(args);
            });

            hub.On("CallAccepted", (string userName) =>
            {
                CommunicatorDelegateArgs args = new CommunicatorDelegateArgs()
                {
                    UserName = userName,
                };
                connection.OnCallAccepted(args);
            });
            
            hub.On("CallDeclined", (string userName) =>
            {
                CommunicatorDelegateArgs args = new CommunicatorDelegateArgs()
                {
                    UserName = userName,
                };
                
                connection.OnCallDeclined(args);
            });

            await hub.StartAsync();

            connection.HubConnection = hub;
            return connection;
        }
    }
}

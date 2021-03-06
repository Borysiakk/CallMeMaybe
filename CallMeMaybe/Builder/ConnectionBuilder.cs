﻿using System;
using System.Threading.Tasks;
using CallMeMaybe.Args;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe.Builder
{
    public static class ConnectionBuilder
    {
        public static async Task<Connection> Create(HttpAuthorizationResult authorization)
        {
            HubConnection hub = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(authorization.Token);
                options.Headers.Add("Id", authorization.Id);
                options.Headers.Add("UserName", authorization.User);
            }).AddMessagePackProtocol().Build();

            Connection connection = new Connection(authorization.User)
            {
                Friends = await HttpRestClient.GetFriendsWithStatus(authorization.Id)
            };


            hub.On("NotificationFriendChangeStatus", async (string userName, bool state) =>
            {
                try
                {
                    var isUserExist = connection.Friends.ContainsKey(userName);
                    if (isUserExist)
                    {
                        connection.Friends[userName] = state;
                        var args = new ChangeStatusFriendDelegateArgs()
                        {
                            Status = state,
                            UserName = userName,
                        };
                        connection.OnChangeStatusFriend(args);
                    }
                    else
                    {
                        await Log.WriteAsync($"Error:Nie znaleziono użytkownika:{userName} którego status miał zostać zmieniony");
                    }
                }   
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });

            hub.On("IncomingCall", async (string userName,string connectionId) =>
            {
                Console.WriteLine("Połączenie przychodzące");
                connection.OnIncomingCall(new ConnectionDelegateArgs(){User = userName});
            });
            
            hub.On("CallAccepted", async (string userName) =>
            {
                Console.WriteLine("Połączenie zaakceptowane");
                connection.Session.UserNameFriend = userName;
                connection.Session.CreateOfferConnection();
            });
            
            hub.On("CallDeclined", async (string userName) =>
            {
                Console.WriteLine("Połączenie odrzucono");
            });
            
            hub.On("ReceivingMessagesAsync", (string username,string message) =>
            {
                var args = new MessageDelegateArgs()
                {
                    UserName = username,
                    Message = message,
                };
            
                connection.OnReceiveMessage(args);
            });

            hub.On("SdpMessageReceivedConfigurationWebRtc",async (string userName,SdpMessage sdpMessage) =>
            {
                await connection.Session.SetRemoteDescriptionAsync(sdpMessage);
                connection.Session.CreateAnswerConnection();
                Console.WriteLine("Odebranie konfiguracji SDP");
            });

            hub.On("IceCandidateReceivedConfigurationWebRtc", (string userName, IceCandidate iceCandidate) =>
            {
                connection.Session.AddIceCandidate(iceCandidate);
                Console.WriteLine("Odebranie konfiguracji ICE");
            });
            
            await hub.StartAsync();
            connection.HubConnection = hub;
            return connection;
        }
    }
}
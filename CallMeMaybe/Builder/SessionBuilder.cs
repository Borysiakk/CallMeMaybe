using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe.Builder
{
    public static class SessionBuilder
    {
        public static Session Create(Connection connection)
        {
            SessionConfigure configure = new SessionConfigure()
            {
                LocalSdpReadyToSend = async message =>
                {
                    await connection.HubConnection.InvokeAsync("SdpMessageSendConfigurationWebRtc",connection.Session.UserNameFriend, message);
                    Console.WriteLine("Wysłanie konfiguracji SDP");
                },
                
                IceCandidateReadyToSend = async candidate =>
                {
                    await connection.HubConnection.InvokeAsync("IceCandidateSendConfigurationWebRtc",connection.Session.UserNameFriend, candidate);
                    Console.WriteLine("Wysłanie konfiguracji ICE");
                },
                
                
            };
            return new Session(configure);
        }
    }
}
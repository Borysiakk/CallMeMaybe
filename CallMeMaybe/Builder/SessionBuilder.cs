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
                    await connection.HubConnection.InvokeAsync("SdpMessageSendConfigurationWebRtc", connection.User, message);
                    Console.WriteLine("Wysłanie konfiguracji SDP");
                },
                
            };

            return new Session(configure);
        }
    }
}
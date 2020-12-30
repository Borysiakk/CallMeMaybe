using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Args;
using Microsoft.AspNetCore.SignalR.Client;

namespace CallMeMaybe
{
    public class Connection :IConnector
    {
        public HubConnection HubConnection;

        public Dictionary<string, bool> Friends { get; set; }
        public AuthorizationManager AuthorizationManager { get; set; }

        public event SendMessageDelegate SendMessage;
        public event ReceiveMessageDelegate ReceiveMessage;
        public event UpdateFriendsStatusDelegate UpdateFriendsStatus;
        
        public delegate void SendMessageDelegate(MessageDelegateArgs args);
        public delegate void ReceiveMessageDelegate(MessageDelegateArgs args);
        public delegate void UpdateFriendsStatusDelegate(UpdateFriendsStatusDelegateArgs args);
        public Connection(ConnectionConfiguration configuration)
        {
            HubConnection = configuration.HubConnection;
        }
        
        public async Task Send(string userName, string message)
        {
            await HubConnection.InvokeAsync("SendMessage",userName, message);
            MessageDelegateArgs args = new MessageDelegateArgs()
            {
                UserName = userName,
                Message = message,
            };
            
            OnSendMessage(args);
        }
        
        public virtual void OnSendMessage(MessageDelegateArgs args)
        {
            SendMessage?.Invoke(args);
        }
        public virtual void OnReceiveMessage(MessageDelegateArgs args)
        {
            ReceiveMessage?.Invoke(args);
        }
        public virtual void OnUpdateFriendsStatus(UpdateFriendsStatusDelegateArgs args)
        {
            UpdateFriendsStatus?.Invoke(args);
        }
    }

}
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Args;
using CallMeMaybe.Builder;
using CallMeMaybe.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe
{
    public class Connection :IConnection
    {
        public string User { get;}
        public Session Session { get; private set; }
        public HubConnection HubConnection { get; set; }
        public Dictionary<string, bool> Friends { get; set; }

        public Connection(string user)
        {
            User = user;
            Session = SessionBuilder.Create(this);
        }
        
        public async Task Call(string userName)
        {
            await HubConnection.InvokeAsync("CallUser", userName);
        }

        public async Task CallAcceptedIncoming(string userName)
        {
            Console.WriteLine("Połączenie zakceptowane");
            await HubConnection.InvokeAsync("AnswerCall", userName, true);
            Session.UserNameFriend = userName;
        }

        public async Task CallDeclinedIncoming(string userName)
        {
            Console.WriteLine("Połączenie odrzucono");
            await HubConnection.InvokeAsync("AnswerCall", userName, false);
        }

        public void OnChangeStatusFriend(ChangeStatusFriendDelegateArgs args)
        {
            ChangeStatusFriend?.Invoke(this,args);
        }
        
        public void OnCallUser(ConnectionDelegateArgs args)
        {
            CallUser?.Invoke(this,args);
        }

        public void OnIncomingCall(ConnectionDelegateArgs args)
        {
            IncomingCall?.Invoke(this,args);
        }

        public void Close()
        {
            Session.Close();
            HubConnection.StopAsync().Wait();
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
        
        public void OnSendMessage(MessageDelegateArgs args)
        {
            SendMessage?.Invoke(this,args);
        }

        public void OnReceiveMessage(MessageDelegateArgs args)
        {
            ReceiveMessage?.Invoke(this,args);
        }

        
        
        public event IConnection.ChangeStatusFriendDelegate ChangeStatusFriend;
        public event IConnectMultimediaCommunication.CallUserDelegate CallUser;
        public event IConnectMultimediaCommunication.IncomingCallDelegate IncomingCall;
        
        public event IConnectionTextCommunication.SendMessageDelegate SendMessage;
        public event IConnectionTextCommunication.ReceiveMessageDelegate ReceiveMessage;
    }
}
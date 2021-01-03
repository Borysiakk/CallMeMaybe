using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Args;
using CallMeMaybe.Interface;
using Microsoft.AspNetCore.SignalR.Client;

namespace CallMeMaybe
{
    public class Connection :IConnection
    {
        public HubConnection HubConnection { get; set; }
        public Dictionary<string, bool> Friends { get; set; }

        public void OnUpdateFriendsStatus(UpdateFriendsStatusDelegateArgs args)
        {
            UpdateFriendsStatus?.Invoke(this,args);
        }

        public event UpdateFriendsStatusDelegate UpdateFriendsStatus;
        
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

        public event SendMessageDelegate SendMessage;
        public event ReceiveMessageDelegate ReceiveMessage;
        public void OnSendMessage(MessageDelegateArgs args)
        {
            SendMessage?.Invoke(this,args);
        }

        public void OnReceiveMessage(MessageDelegateArgs args)
        {
            ReceiveMessage?.Invoke(this,args);
        }

        public async Task Call(string userName)
        {
            await HubConnection.InvokeAsync("CallUser",userName);
            
            var communicatorDelegateArgs = new CommunicatorDelegateArgs()
            {
                UserName = userName,
            };
            
            OnCallUser(communicatorDelegateArgs);
        }

        public async Task AnswerCall(string userName, bool acceptCall)
        {
            await HubConnection.InvokeAsync("AnswerCall",userName,acceptCall);
            var communicatorDelegateArgs = new CommunicatorDelegateArgs()
            {
                UserName = userName,
            };
            OnAnswerUserCall(communicatorDelegateArgs);
        }

        public void OnCallUser(CommunicatorDelegateArgs args)
        {
            CallUser?.Invoke(this,args);
        }

        public void OnIncomingCall(CommunicatorDelegateArgs args)
        {
            IncomingCall?.Invoke(this,args);
        }

        public void OnCallAccepted(CommunicatorDelegateArgs args)
        {
            CallUserAccepted?.Invoke(this,args);
        }

        public void OnCallDeclined(CommunicatorDelegateArgs args)
        {
            CallUserDeclined?.Invoke(this,args);
        }

        public void OnAnswerUserCall(CommunicatorDelegateArgs args)
        {
            AnswerUserCall?.Invoke(this,args);
        }

        public event CallUserDelegate CallUser;
        public event IncomingCallDelegate IncomingCall;
        public event AnswerUserCallDelegate AnswerUserCall;
        public event CallAcceptedDelegate CallUserAccepted;
        public event CallDeclinedDelegate CallUserDeclined;
    }
    
}
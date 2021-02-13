using System;
using System.Collections;
using System.Threading.Tasks;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.SignalR.Interface;
using Microsoft.AspNetCore.SignalR;

namespace CallMeMaybe.SignalR.Hubs
{
    public class CommunicationServerHubs :Hub<IConnection>
    {
        private readonly IFriendsService _friendsService;
        private readonly ISessionService _sessionService;
        
        public CommunicationServerHubs(IFriendsService friendsService, ISessionService sessionService)
        {
            _friendsService = friendsService;
            _sessionService = sessionService;
        }

        public override async Task OnConnectedAsync()
        {           
            
            var id = Context.GetHttpContext().Request.Headers["Id"].ToString();
            var userName = Context.GetHttpContext().Request.Headers["UserName"].ToString();

            SessionUser sessionUser = new SessionUser()
            {
                UserId = id,
                UserName = userName,
                StatusConnection = true,
                ConnectionId = Context.ConnectionId,
            };

            var activeFriends = _friendsService.GetConnectionIdActiveFriends(id);
            if (activeFriends != null)
            {
                await Clients.Clients(activeFriends).NotificationFriendChangeStatus(userName, true);
            }

            await _sessionService.AddAsync(sessionUser);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string user = _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            var activeFriends = _friendsService.GetActiveFriendByUserName(user);
            
            if (activeFriends != null)
            {
                await Clients.Clients(activeFriends).NotificationFriendChangeStatus(user, false);
            }

            _sessionService.CloseSessionUser(user);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task CallUser(string userNameFriend)
        {
            var userName = _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            var connectionIdFriend = _sessionService.GetConnectionIdByUserName(userNameFriend);
            await Clients.Client(connectionIdFriend).IncomingCall(userName,Context.ConnectionId);
        }

        public async Task AnswerCall(string userFriendCaller,bool acceptCall)
        {
            string connectedIdCaller = _sessionService.GetConnectionIdByUserName(userFriendCaller);
            string userNameCurrent = _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            
            if (acceptCall) await Clients.Client(connectedIdCaller).CallAccepted(userNameCurrent);
            else await Clients.Client(connectedIdCaller).CallDeclined(userNameCurrent);
        }

        public async Task SendMessage(string userName, string message)
        {
            Console.WriteLine("Wysyłanie wiadomosci");
            string connectionId =  _sessionService.GetConnectionIdByUserName(userName);
            string userNameCurrent = _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            await Clients.Client(connectionId).ReceivingMessagesAsync(userNameCurrent,message);
        }
    }
}
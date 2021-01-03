using System;
using System.Threading.Tasks;
using CallMeMaybe.Domain;
using CallMeMaybe.Domain.Entities;
using CallMeMaybe.Infrastructure.Interface;
using CallMeMaybe.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;

namespace CallMeMaybe.SignalR.Hubs
{
    
    public class CallMeMaybeHub :Hub<IClient>
    {
        private readonly IFriendService _friendService;
        private readonly ISessionService _sessionService;

        public CallMeMaybeHub(IFriendService friendService, ISessionService sessionService)
        {
            _friendService = friendService;
            _sessionService = sessionService;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("OK");
            var id = Context.GetHttpContext().Request.Headers["Id"];
            var userName = Context.GetHttpContext().Request.Headers["UserName"];

            Session session = new Session()
            {
                UserId = id,
                Status = true,
                ConnectionId = Context.ConnectionId,
            };

            var activeFriends = await _friendService.GetConnectionIdActiveFriendsAsync(id);
            if (activeFriends != null) 
                await Clients.Clients(activeFriends).FriendStatusNotification(userName,true);

            await _sessionService.AddAsync(session);
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id  = await _sessionService.GetUserIdByConnectionId(Context.ConnectionId);
            var userName  = await _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            
            var activeFriends = await _friendService.GetConnectionIdActiveFriendsAsync(id);
            await Clients.Clients(activeFriends).FriendStatusNotification(userName,false);
            await _sessionService.UpdateStatusAsync(Context.ConnectionId,false);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string userName,string mgs)
        {
            string connectionId = await _sessionService.GetConnectionIdByUserName(userName);
            string userNameCurrent = await _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            await Clients.Client(connectionId).BroadcastMessage(userNameCurrent,mgs);
        }

        public async Task CallUser(string userName)
        {
            Console.WriteLine("CallUser : " + userName);
            string connectionId = await _sessionService.GetConnectionIdByUserName(userName);
            string userNameCurrent = await _sessionService.GetUserNameByConnectionId(Context.ConnectionId);

            await Clients.Client(connectionId).IncomingCall(userNameCurrent);
        }

        public async Task AnswerCall(string userName,bool acceptCall)
        {
            Console.WriteLine(acceptCall);
            string connectionId = await _sessionService.GetConnectionIdByUserName(userName);
            string userNameCurrent = await _sessionService.GetUserNameByConnectionId(Context.ConnectionId);
            
            if (acceptCall)
            {
                await Clients.Client(connectionId).CallAccepted(userNameCurrent);
            }
            else
            {
                await Clients.Client(connectionId).CallDeclined(userNameCurrent);
            }
        }
    }

    public interface IClient :IClientMultimedia
    {
        Task BroadcastMessage(string username,string msg);
        Task FriendStatusNotification(string username,bool state);

    }

    public interface IClientMultimedia
    {
        Task CallAccepted(string userName);
        Task CallDeclined(string userName);
        Task IncomingCall(string userName);
    }
}
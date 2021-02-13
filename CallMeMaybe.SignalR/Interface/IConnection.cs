using System.Threading.Tasks;

namespace CallMeMaybe.SignalR.Interface
{
    public interface IConnection :IConnectionTextCommunication,IConnectMultimediaCommunication
    {
        Task NotificationFriendChangeStatus(string userName, bool status);
    }
}
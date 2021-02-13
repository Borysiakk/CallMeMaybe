using System.Threading.Tasks;

namespace CallMeMaybe.SignalR.Interface
{
    public interface IConnectionTextCommunication
    {
        Task ReceivingMessagesAsync(string username, string msg);
    }
}
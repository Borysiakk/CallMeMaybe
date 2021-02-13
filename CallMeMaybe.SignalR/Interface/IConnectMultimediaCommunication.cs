using System.Threading.Tasks;

namespace CallMeMaybe.SignalR.Interface
{
    public interface IConnectMultimediaCommunication
    {
        Task CallAccepted(string userName);
        Task CallDeclined(string userName);
        Task IncomingCall(string userName,string connectionId);
    }
}
using System.Threading.Tasks;
using CallMeMaybe.Args;

namespace CallMeMaybe.Interface
{
    public interface IConnectMultimediaCommunication
    {
        public Task Call(string userName);
        public Task CallAcceptedIncoming(string userName);
        public Task CallDeclinedIncoming(string userName);
        public void OnCallUser(ConnectionDelegateArgs args);
        public void OnIncomingCall(ConnectionDelegateArgs args);
        
        public delegate void CallUserDelegate(object sender, ConnectionDelegateArgs args);
        public delegate void IncomingCallDelegate(object sender,ConnectionDelegateArgs args);
    }
}
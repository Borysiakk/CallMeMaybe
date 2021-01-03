using System.Threading.Tasks;
using CallMeMaybe.Args;

namespace CallMeMaybe.Interface
{
    public interface IConnectionMultimediaCommunication
    {
        public Task Call(string userName);
        public Task AnswerCall(string userName, bool acceptCall);

        public void OnCallUser(CommunicatorDelegateArgs args);
        public void OnIncomingCall(CommunicatorDelegateArgs args);
        public void OnCallAccepted(CommunicatorDelegateArgs args);
        public void OnCallDeclined(CommunicatorDelegateArgs args);
        public void OnAnswerUserCall(CommunicatorDelegateArgs args);
        
        public event CallUserDelegate CallUser;
        public event IncomingCallDelegate IncomingCall;
        public event AnswerUserCallDelegate AnswerUserCall;
        public event CallAcceptedDelegate CallUserAccepted;
        public event CallDeclinedDelegate CallUserDeclined;
    }

    public delegate void CallUserDelegate(object sender, CommunicatorDelegateArgs args);
    public delegate void IncomingCallDelegate(object sender, CommunicatorDelegateArgs args);
    public delegate void CallAcceptedDelegate(object sender, CommunicatorDelegateArgs args);
    public delegate void CallDeclinedDelegate(object sender, CommunicatorDelegateArgs args);
    public delegate void AnswerUserCallDelegate(object sender, CommunicatorDelegateArgs args);
}
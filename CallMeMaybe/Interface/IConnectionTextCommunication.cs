using System.Threading.Tasks;
using CallMeMaybe.Args;

namespace CallMeMaybe.Interface
{
    public interface IConnectionTextCommunication
    {
        Task Send(string userName, string message);
        event SendMessageDelegate SendMessage;
        event ReceiveMessageDelegate ReceiveMessage;

        void OnSendMessage(MessageDelegateArgs args);
        void OnReceiveMessage(MessageDelegateArgs args);
    }
    
    public delegate void SendMessageDelegate(object sender, MessageDelegateArgs args);
    public delegate void ReceiveMessageDelegate(object sender, MessageDelegateArgs args);
}
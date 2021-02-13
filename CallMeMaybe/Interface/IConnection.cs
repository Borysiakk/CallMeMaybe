using CallMeMaybe.Args;

namespace CallMeMaybe.Interface
{
    public interface IConnection :IConnectionTextCommunication,IConnectMultimediaCommunication
    {
        public delegate void ChangeStatusFriendDelegate(object sender, ChangeStatusFriendDelegateArgs args);
    }
}
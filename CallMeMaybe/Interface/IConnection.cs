using CallMeMaybe.Args;

namespace CallMeMaybe.Interface
{
    public interface IConnection :IConnectionTextCommunication,IConnectionMultimediaCommunication
    {
        void OnUpdateFriendsStatus(UpdateFriendsStatusDelegateArgs args);
        event UpdateFriendsStatusDelegate UpdateFriendsStatus;
    }

    public delegate void UpdateFriendsStatusDelegate(object sender, UpdateFriendsStatusDelegateArgs args);
}
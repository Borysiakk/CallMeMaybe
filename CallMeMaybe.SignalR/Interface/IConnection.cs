using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe.SignalR.Interface
{
    public interface IConnection :IConnectionTextCommunication,IConnectMultimediaCommunication
    {
        Task SdpMessageReceivedConfigurationWebRtc(string userName,SdpMessage sdpMessage);
        Task IceCandidateReceivedConfigurationWebRtc(string userName, IceCandidate iceCandidate);
        
        Task NotificationFriendChangeStatus(string userName, bool status);
        
    }
}
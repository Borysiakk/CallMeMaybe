using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe.Interface
{
    public interface IConnectionSession
    {
        public void Close();
        public Task Initialization();
        public void CreateOfferConnection();
        public void AddIceCandidate(IceCandidate candidate);
        public Task SetRemoteDescriptionAsync(SdpMessage message);
    }
}
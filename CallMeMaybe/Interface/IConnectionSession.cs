using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe.Interface
{
    public interface IConnectionSession
    {
        public void Close();
        public Task Initialization(string user);
        public void CreateOfferConnection();
        public void CreateAnswerConnection();
        public void AddIceCandidate(IceCandidate candidate);
        public Task SetRemoteDescriptionAsync(SdpMessage message);
    }
}
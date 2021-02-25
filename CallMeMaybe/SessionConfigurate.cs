using System;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe
{
    public class SessionConfigure
    {
        public Action Connected { get; set; }
        public PeerConnection.IceStateChangedDelegate IceStateChanged { get; set; }
        public PeerConnection.IceCandidateReadytoSendDelegate IceCandidateReadyToSend { get; set; }
        public PeerConnection.IceGatheringStateChangedDelegate IceGatheringStateChanged { get; set; }
        
        public PeerConnection.LocalSdpReadyToSendDelegate LocalSdpReadyToSend { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Interface;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe
{
    public sealed class Session :ISession,IConnectionSession
    {
        public PeerConnection Connection { get; set; }
        public Session(SessionConfigure configure)
        {
            Connection = new PeerConnection();
            Connection.Connected += configure.Connected;
            Connection.IceStateChanged += configure.IceStateChanged;
            Connection.LocalSdpReadytoSend += configure.LocalSdpReadyToSend;
            Connection.IceCandidateReadytoSend += configure.IceCandidateReadyToSend;
            Connection.IceGatheringStateChanged += configure.IceGatheringStateChanged;
        }

        public void Close()
        {
            Connection.Close();
        }

        public async Task Initialization()
        {
            var config = new PeerConnectionConfiguration
            {
                IceServers = new List<IceServer> {new IceServer{ Urls = { "stun:stun.l.google.com:19302" } } }
            };
            await Connection.InitializeAsync(config);
            Console.WriteLine("Peer connection initialized.");
        }

        public void CreateOfferConnection()
        {
            Connection.CreateOffer();
        }

        public void AddIceCandidate(IceCandidate candidate)
        {
            Connection.AddIceCandidate(candidate);
        }

        public async Task SetRemoteDescriptionAsync(SdpMessage message)
        {
            await Connection.SetRemoteDescriptionAsync(message);
            if (message.Type == SdpMessageType.Offer)
            {
                Connection.CreateAnswer();
            }
        }
    }
}
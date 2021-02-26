using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallMeMaybe.Interface;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe
{
    public sealed class Session :ISession,IConnectionSession
    {
        private Transceiver audioTransceiver;
        private LocalAudioTrack localAudioTrack;
        private AudioTrackSource microphoneSource;
        
        public string UserNameFriend { get; set; }
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
            localAudioTrack?.Dispose();
            microphoneSource?.Dispose();
            Connection.Close();
        }

        public async Task Initialization(string user)
        {
            try
            {
                var config = new PeerConnectionConfiguration
                {
                    IceServers = new List<IceServer> {new IceServer{ Urls = { "stun:stun.l.google.com:19302" } } }
                };
                await Connection.InitializeAsync(config);
            
                microphoneSource = await DeviceAudioTrackSource.CreateAsync();
                var audioTrackConfig = new LocalAudioTrackInitConfig { trackName = "microphone_track" };
                localAudioTrack = LocalAudioTrack.CreateFromSource(microphoneSource, audioTrackConfig);
                audioTransceiver = Connection.AddTransceiver(MediaKind.Audio);
                audioTransceiver.LocalAudioTrack = localAudioTrack;
                audioTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
            
                Console.WriteLine("Peer connection initialized.");
            }
            catch (Exception e)
            {
                await Log.WriteAsync(e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        public void CreateOfferConnection()
        {
            Connection.CreateOffer();
        }

        public void CreateAnswerConnection()
        {
            Connection.CreateAnswer();
        }

        public void AddIceCandidate(IceCandidate candidate)
        {
            Connection.AddIceCandidate(candidate);
        }

        public async Task SetRemoteDescriptionAsync(SdpMessage message)
        {
            await Connection.SetRemoteDescriptionAsync(message);
        }
    }
}
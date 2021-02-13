using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace CallMeMaybe
{
    public class CallConnection
    {
        public PeerConnection PeerConnection { get; set; }

        private Transceiver _audioTransceiver;
        //private Transceiver _videoTransceiver;
        private LocalAudioTrack _localAudioTrack;
        //private LocalVideoTrack _localVideoTrack;
        private AudioTrackSource _audioTrackSource;
        //private VideoTrackSource _videoTrackSource;
        private IReadOnlyList<VideoCaptureDevice> _deviceList;
        private PeerConnectionConfiguration Configuration { get; set; }

        public CallConnection()
        {
            Configuration = new PeerConnectionConfiguration()
            {
                IceServers = new List<IceServer>
                {
                    new IceServer {Urls = {"stun:stun.l.google.com:19302"}}
                }
            };
            
        }

        public async Task InitializationAsync()
        {
            _deviceList = await DeviceVideoTrackSource.GetCaptureDevicesAsync();
            PeerConnection = new PeerConnection();
            await PeerConnection.InitializeAsync(Configuration);
            
            _audioTrackSource = await DeviceAudioTrackSource.CreateAsync();
            var trackSettings = new LocalAudioTrackInitConfig { trackName = "mic_track" };
            _localAudioTrack = LocalAudioTrack.CreateFromSource(_audioTrackSource,trackSettings);
            
            _audioTransceiver = PeerConnection.AddTransceiver(MediaKind.Audio);
            _audioTransceiver.DesiredDirection = Transceiver.Direction.SendReceive;
            _audioTransceiver.LocalAudioTrack = _localAudioTrack;
        }
    }
}
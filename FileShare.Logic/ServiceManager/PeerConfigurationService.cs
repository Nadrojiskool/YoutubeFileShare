using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using FileShare.Contracts.Services;
using FileShare.Domain.Models;

namespace FileShare.Logic.ServiceManager
{
    public class PeerConfigurationService : IPeerConfigurationService<PingService>
    {
        #region field
        private int _port;
        private ICommunicationObject Communication;
        private DuplexChannelFactory<IPingService> _factory;
        private bool _isServiceStarted;
        #endregion

        #region Cto
        public PeerConfigurationService(Peer<IPingService> peer)
        {
            Peer = peer;
            PingService = new PingService();
        }
        #endregion

        public int Port => FindFreePort();

        public int FindFreePort()
        {
            int port;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socket.Bind(endPoint);
                IPEndPoint local = (IPEndPoint)socket.LocalEndPoint;
                port = local.Port;
            }
            
            if (port == 0)
            {
                throw new ArgumentNullException(nameof(port));
            }

            return port;
        }

        public Peer<IPingService> Peer { get; }

        public PingService PingService { get; set; }

        public bool StartPeerService()
        {
        #pragma warning disable 618
            var binding = new NetPeerTcpBinding
            {
                Security = { Mode = SecurityMode.None }
            };
        #pragma warning restore 618
            var endpoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(IPingService)), binding, new EndpointAddress("net.p2p://YoutubeFileShare"));
            Peer.Host = PingService;
            _factory = new DuplexChannelFactory<IPingService>(new InstanceContext(Peer.Host), endpoint);
            Peer.Channel = _factory.CreateChannel();
            Communication = (ICommunicationObject)Peer.Channel;

            if (Communication != null)
            {
                Communication.Opened += CommunicationOnOpened;
                try
                {
                    Communication.Open();
                    if (_isServiceStarted) return _isServiceStarted;
                }
                catch (PeerToPeerException e)
                {
                    throw new PeerToPeerException($"error establishing peer services");
                }
            }

            return _isServiceStarted;
        }

        public bool StopPeerService()
        {
            if (Communication != null)
            {
                Communication.Close();
                Communication = null;
                _factory = null;
                return true;
            }

            return false;
        }

        private void CommunicationOnOpened(object sender, EventArgs eventArgs)
        {
            _isServiceStarted = true;
        }
    }
}

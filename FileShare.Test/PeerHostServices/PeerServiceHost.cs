using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileShare.Contracts.FileShare;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domain.Models;
using FileShare.Logic.ServiceManager;
using FileShare.Logic.FileShareManager;

namespace FileShare.Test.PeerHostServices
{
    public class PeerServiceHost
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        private bool _isStarted = false;
        private int _port = 0;
        FileShareManager _file = new FileShareManager();
        Dictionary<string, HostInfo> _currentHost = new Dictionary<string, HostInfo>();

        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
            _port = ConfigurePeer.Port;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }

        public void RunPeerServiceHost(Peer<IPingService> peer)
        {
            if (peer == null)
            {
                throw new ArgumentNullException(nameof(peer));
            }
            
            RegisterPeer.StartPeerRegistration(peer.PeerID, _port);

            if (RegisterPeer.IsPeerRegistered)
            {
                Console.WriteLine($"{peer.Username} Registration Completed.");
                Console.WriteLine($"Peer Uri : {RegisterPeer.PeerUri}   Port : {_port}");
            }

            if (ResolvePeer != null)
            {
                Console.WriteLine($"Resolving Peer {peer.Username}..");
                ResolvePeer.ResolvePeerName(peer.PeerID);
                var result = ResolvePeer.PeerEndpointCollection;

                if (ConfigurePeer.StartPeerService())
                {
                    Console.WriteLine($"Peer Services Started.");
                    peer.Channel.Ping(new HostInfo
                    {
                        ID = peer.PeerID,
                        Port = _port,
                        Uri = RegisterPeer.PeerUri
                    });

                    if (ConfigurePeer.PingService != null)
                    {
                        ConfigurePeer.PingService.PeerEndpointInformation += PingServiceOnPeerEndpointInformation;
                    }

                    if (StartFileShareService(_port, RegisterPeer.PeerUri))
                    {
                        Console.WriteLine("File Service Host Started..");
                    }
                }
                else
                {
                    Console.WriteLine($"Error Starting Peer Serivce.");
                }
            }
        }
        private void PingServiceOnPeerEndpointInformation(HostInfo endpointInfo)
        {
            Console.Clear();
            if (!_currentHost.Any())
            {
                var uri = $"net.tcp://{endpointInfo.Uri}:{endpointInfo.Port}/FileShare";
                var callback = new InstanceContext(new FileShareCallback());
                var binding = new NetTcpBinding(SecurityMode.None);
                var channel = new DuplexChannelFactory<IFileShareService>(callback, binding);
                var endpoint = new EndpointAddress(uri);
                var proxy = channel.CreateChannel(endpoint);
                if (proxy != null)
                {
                    HostInfo info = new HostInfo
                    {
                        ID = ConfigurePeer.Peer.PeerID,
                        Port = _port,
                        Uri = RegisterPeer.PeerUri
                    };
                    proxy.PingHostService(info);
                    Console.WriteLine($"{_currentHost.Count} Host(s) Currently Connected.");
                    _currentHost.ToList().ForEach(p =>
                    {
                        Console.WriteLine($"Host ID : {p.Key}   Endpoint : {p.Value.Uri}:{p.Value.Port}");
                    });
                }
            }
            else
            {
                if (_currentHost.Any(p => p.Key == endpointInfo.ID))
                {
                    Console.WriteLine("Host Already Exists.");
                }
                else
                {
                    var uri = $"net.tcp://{endpointInfo.Uri}:{endpointInfo.Port}/FileShare";
                    var callback = new InstanceContext(new FileShareCallback());
                    var binding = new NetTcpBinding(SecurityMode.None);
                    var channel = new DuplexChannelFactory<IFileShareService>(callback, binding);
                    var endpoint = new EndpointAddress(uri);
                    var proxy = channel.CreateChannel(endpoint);
                    if (proxy != null)
                    {
                        HostInfo info = new HostInfo
                        {
                            ID = ConfigurePeer.Peer.PeerID,
                            Port = _port,
                            Uri = RegisterPeer.PeerUri
                        };
                        proxy.PingHostService(info);
                        Console.WriteLine($"{_currentHost.Count} Host(s) Currently Connected.");
                        _currentHost.ToList().ForEach(p =>
                        {
                            Console.WriteLine($"Host ID : {p.Key}   Endpoint : {p.Value.Uri}:{p.Value.Port}");
                        });
                    }
                }
            }
        }

        public bool StartFileShareService(int port, string uri)
        {
            if (uri.Any() && _port > 0)
            {
                Uri[] uris = new Uri[1];
                var address = $"net.tcp://{uri}:{port}/FileShare";
                uris[0] = new Uri(address);
                IFileShareService fileshare = _file;
                var host = new ServiceHost(fileshare, uris);
                var binding = new NetTcpBinding(SecurityMode.None);
                host.AddServiceEndpoint(typeof(IFileShareService), binding, string.Empty);
                host.Opened += HostOnOpened;
                _file.CurrentHostUpdate += FileOnCurrentHostUpdate;
                host.Open();
                return _isStarted;
            }

            return false;
        }

        private void FileOnCurrentHostUpdate(HostInfo info)
        {
            Console.Clear();
            if (info != null && _currentHost.All(p => p.Key != info.ID))
            {
                _currentHost.Add(info.ID, info);
                Console.WriteLine($"{_currentHost.Count} Host(s) Currently Available.");
                _currentHost.ToList().ForEach(p =>
                {
                    Console.WriteLine($"Host ID : {p.Key}   Endpoint : {p.Value.Uri}:{p.Value.Port}");
                });
            }
            else if (!_currentHost.Any())
            {
                if (info != null) _currentHost.Add(info.ID, info);
                Console.WriteLine($"{_currentHost.Count} Host(s) Currently Available.");
                _currentHost.ToList().ForEach(p =>
                {
                    Console.WriteLine($"Host ID : {p.Key}   Endpoint : {p.Value.Uri}:{p.Value.Port}");
                });
            }
        }

        private void HostOnOpened(object sender, EventArgs eventArgs)
        {
            _isStarted = true;
        }
    }
}

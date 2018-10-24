using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Domain.Models;
using FileShare.Logic.ServiceManager;

namespace FileShare.Presentation.PnrpManager
{
    public class PeerServiceHost
    {
        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }

        public void RunPeerServices()
        {
            if (ConfigurePeer.Peer != null)
            {

            }
        }
    }
}

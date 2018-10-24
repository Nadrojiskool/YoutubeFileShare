using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.PeerToPeer;
using FileShare.Contracts.Repository;
using FileShare.Domain.Models;

namespace FileShare.Logic.PnrpManager
{
    public class PeerNameResolver : IPeerNameResolverRepository
    {
        private PeerEndpointCollection _peers;
        private string _username;

        public PeerNameResolver(string username)
        {
            _username = username;
        }

        public void ResolvePeerName(string peerID)
        {
            if (string.IsNullOrEmpty(_username))
            {
                throw new ArgumentNullException(nameof(_username));
            }

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(peerID, PeerNameType.Unsecured), Cloud.AllLinkLocal);

            /* if (result.Any())
            {
                PeerEndPointCollection = new PeerEndPointCollection(result[0].PeerName, result[0].EndPointCollection);
            } */
        }

        public PeerEndpointCollection PeerEndpointCollection { get; set; }
    }
}

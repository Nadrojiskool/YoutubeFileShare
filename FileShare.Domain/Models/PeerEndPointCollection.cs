using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.PeerToPeer;
using System.Collections.ObjectModel;

namespace FileShare.Domain.Models
{
    public class PeerEndpointCollection
    {
        public PeerEndpointCollection(PeerName peer)
        {
            PeerHostName = peer;
            PeerEndpoints = new ObservableCollection<PeerEndpointInfo>();
        }

        public PeerName PeerHostName { get; set; }
        public ObservableCollection<PeerEndpointInfo> PeerEndpoints { get; set; }
    }
}

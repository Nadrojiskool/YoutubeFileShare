using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FileShare.Domain.Models
{
    public class PeerEndpointInfo
    {
        public string PeerUri { get; set; }
        public IPEndPointCollection PeerIPCollection { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

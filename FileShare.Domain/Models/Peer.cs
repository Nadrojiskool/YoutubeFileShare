using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.PeerToPeer;

namespace FileShare.Domain.Models
{
    public class Peer<T>
    {
        public string PeerID { get; set; }

        public string Username { get; set; }

        public PeerName PeerName { get; set; }

        public T Channel { get; set; }

        public T Host { get; set; }
    }
}

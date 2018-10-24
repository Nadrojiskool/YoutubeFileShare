using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.Models;

namespace FileShare.Contracts.Services
{
    public interface IPeerConfigurationService<T>
    {
        T PingService { get; set; }
        int Port { get; }
        Peer<IPingService> Peer { get; }

        bool StartPeerService();
        bool StopPeerService();
    }
}

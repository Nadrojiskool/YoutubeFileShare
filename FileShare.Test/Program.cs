using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using FileShare.Contracts.Repository;
using FileShare.Contracts.Services;
using FileShare.Logic.PnrpManager;
using FileShare.Logic.ServiceManager;
using FileShare.Domain.Models;
using FileShare.Test.PeerHostServices;

namespace FileShare.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() < 3)
            {
                Process.Start("FileShare.Test.exe");
            }

            new Program().Run();
        }

        private void Run()
        {
            Console.WriteLine("Hello! Enter Your Username:");
            string username = Console.ReadLine();

            Peer<IPingService> peer = new Peer<IPingService>
            {
                PeerID = Guid.NewGuid().ToString().Split('-')[4],
                Username = username
            };
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.PeerID);
            IPeerConfigurationService<PingService> peerConfigurationService = new PeerConfigurationService(peer);
            
            PeerServiceHost psh = new PeerServiceHost(peerRegistration, peerNameResolver, peerConfigurationService);

            Thread thd = new Thread(() => 
            {
                psh.RunPeerServiceHost(peer);
            }) { IsBackground = true };
            thd.Start();

            Console.ReadLine();
        }
    }
}

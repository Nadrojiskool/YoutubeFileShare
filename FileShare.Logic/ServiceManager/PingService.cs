using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using FileShare.Contracts.Services;
using FileShare.Domain.FileSearch;
using FileShare.Domain.Models;
using FileShare.SampleData;

namespace FileShare.Logic.ServiceManager
{
    public delegate void OnPeerInfo(HostInfo endpointInfo);
    public delegate void FileSearchResultDelegate(FileSearchResultModel fileSearch);

    public class PingService : IPingService
    {
        public event OnPeerInfo PeerEndpointInformation;
        public event FileSearchResultDelegate FileSearchResult;
        private Random rnd;
        private int _count = new FileSample().GetFileMetaData().Count;

        public PingService()
        {
            rnd = new Random();
            ClientHostDetails = new ObservableCollection<HostInfo>();
            AvailableFileMetaData = new FileSample().GetFileMetaData().Take(rnd.Next(1, _count)).ToList();
        }

        /*public PingService(HostInfo info)
        {
            FileServiceHost = info;
            ClientHostDetails = new ObservableCollection<HostInfo>();
        }*/

        public void Ping (HostInfo info)
        {
            var host = Dns.GetHostEntry(info.Uri);
            IPEndPointCollection ips = new IPEndPointCollection();
            host.AddressList.ToList()?.ForEach(p => { ips.Add(new IPEndPoint(p, info.Port)); });
            var peerInfo = new PeerEndpointInfo
            {
                LastUpdated = DateTime.Now,
                PeerUri = info.Uri,
                PeerIPCollection = ips
            };
            PeerEndpointInformation?.Invoke(info);
            /*var host = Dns.GetHostEntry(peerUri);
            Console.WriteLine($"New Peer Entered: Peer Endpoint Details");
            host.AddressList.ToList()?.ForEach(p => Console.WriteLine($"\t \t \t Endpoint : {p}:{port}"));*/
            // Console.WriteLine($"Yay~! -- {peerUri}");
        }
        
        public HostInfo FileServiceHost { get; set; }
        public IList<FileMetaData> AvailableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }

        public void SearchFiles(string searchTerm, string peerID)
        {
            if (ClientHostDetails.Any()) // Removed in Tutorial //
            {
                // var info = ClientHostDetails.First(p => p.ID == peerID);
                var result = (from file in AvailableFileMetaData
                        where searchTerm == file.FileName
                              || file.FileName.Contains(searchTerm)
                              || file.FileName.IndexOf(searchTerm, StringComparison.CurrentCulture) > 0
                        select file);

                if (result.Any())
                {
                    FileSearchResultModel searchResult = new FileSearchResultModel
                    {
                        PeerID = peerID,
                        Files = (ObservableCollection<FileMetaData>) result
                    };

                    FileSearchResult?.Invoke(searchResult);
                }
                /*if (info != null)
                {
                    if (result.Any())
                    {
                        FileSearchResultModel search = new FileSearchResultModel
                        {
                            ServiceHost = FileServiceHost,
                            Files = (ObservableCollection<FileMetaData>)result
                        };
                    }
                }*/
            }
        }
    }
}

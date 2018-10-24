using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FileShare.Contracts.FileShare;
using FileShare.Logic.FileShareManager;

namespace FileShare.Presentation.FileShareServices.FileShareHostService
{
    public class FileShareHostService : IFileShareHostService
    {
        private ServiceHost _host;

        public FileShareHostService(int port, string uri)
        {
            Port = port;
            Uri = uri;
            IsStarted = false;
        }

        public int Port { get; }
        public string Uri { get; }
        public bool IsStarted { get; set; }
        
        public bool Stop()
        {
            if (_host != null)
            {
                _host.Closed += HostOnClosed;
                _host.Close();
                _host = null;
                return IsStarted;
            }
            return IsStarted;
        }

        private void HostOnClosed(object sender, EventArgs eventArgs)
        {
            IsStarted = true;
        }

        public bool Start()
        {
            var uri = new Uri[1];
            if (!string.IsNullOrEmpty(Uri) && Port > 0)
            {
                var address = $"net.tcp://{Uri}:{Port}/Fileshare";
                uri[0] = new Uri(address);
                IFileShareService fileShare = new FileShareManager();
                _host = new ServiceHost(fileShare, uri);
                var binding = new NetTcpBinding(SecurityMode.None);
                _host.AddServiceEndpoint(typeof(IFileShareService), binding, "");
                _host.Opened += HostOnOpened;
                _host.Open();
                return IsStarted;
            }
            return IsStarted;
        }

        private void HostOnOpened(object sender, EventArgs eventArgs)
        {
            IsStarted = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FileShare.Contracts.FileShare;
using FileShare.Domain.FileSearch;
using FileShare.Domain.Models;

namespace FileShare.Logic.FileShareManager
{
    public delegate void CurrentHostInfo(HostInfo info, bool isCallback = false);
    public delegate void CurrentClientInfo(string peerID, IFileShareServiceCallback callback);

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class FileShareManager : IFileShareService
    {
        private Dictionary<string, HostInfo> _currentHost = new Dictionary<string, HostInfo>();
        public event CurrentHostInfo CurrentHostUpdate;

        public FilePartModel GetAllFileByte(FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public void ForwardResult(FileSearchResultModel result)
        {

        }
        public void PingHostService(HostInfo info, bool isCallback)
        {
            //Console.Write($"Peer : {info.ID}    Server : {info.Uri}:{info.Port}\n");
            var callback = OperationContext.Current.GetCallbackChannel<IFileShareServiceCallback>();
            if (callback != null)
            {
                if (isCallback)
                {
                    if(callback.IsConnected($"Ping Back Direct Connection: {DateTime.UtcNow:T}"))
                    {
                        info.Callback = callback;
                        CurrentHostUpdate?.Invoke(info, true);
                    }
                }
                else
                {
                    if (callback.IsConnected($"Direct Peer Connection Established at {DateTime.UtcNow:D}"))
                    {
                        _currentHost.Add(info.ID, info);
                        info.Callback = callback;
                        CurrentHostUpdate?.Invoke(info);
                    }
                }

            }
        }
    }
}

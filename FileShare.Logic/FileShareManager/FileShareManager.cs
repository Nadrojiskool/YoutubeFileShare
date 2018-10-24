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
    public delegate void CurrentHostInfo(HostInfo info);

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
        public void PingHostService(HostInfo info)
        {
            //Console.Write($"Peer : {info.ID}    Server : {info.Uri}:{info.Port}\n");
            var callback = OperationContext.Current.GetCallbackChannel<IFileShareServiceCallback>();
            if (callback != null)
            {
                if (callback.IsConnected($"Message from Server at {DateTime.UtcNow:D}"))
                {
                    _currentHost.Add(info.ID, info);
                    CurrentHostUpdate?.Invoke(info);
                }
            }
        }
    }
}

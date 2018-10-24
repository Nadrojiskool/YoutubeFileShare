using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FileShare.Domain.FileSearch;
using FileShare.Domain.Models;

namespace FileShare.Contracts.FileShare
{
    [ServiceContract(CallbackContract = typeof(IFileShareServiceCallback), SessionMode = SessionMode.Required)]

    public interface IFileShareService
    {
        [OperationContract(IsOneWay = false)]
        FilePartModel GetAllFileByte(FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta);

        [OperationContract(IsOneWay = true)]
        void PingHostService(HostInfo info, bool isCallback = false);

        [OperationContract(IsOneWay = true)]
        void ForwardResult(FileSearchResultModel result);
    }
}

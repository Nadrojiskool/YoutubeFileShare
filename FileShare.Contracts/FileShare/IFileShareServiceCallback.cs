using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.FileSearch;

namespace FileShare.Contracts.FileShare
{
    public interface IFileShareServiceCallback
    {
        [OperationContract(IsOneWay = false)]
        bool IsConnected(string replyMessage);

        [OperationContract(IsOneWay = false)]
        bool ForwardSearchResult(FileSearchResultModel searchResults);
    }
}

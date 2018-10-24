using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Contracts.FileShare;
using FileShare.Domain.FileSearch;

namespace FileShare.Test.PeerHostServices
{
    class FileShareCallback : IFileShareServiceCallback
    {
        public bool IsConnected (string replyMessage)
        {
            if (!string.IsNullOrEmpty(replyMessage))
            {
                Console.WriteLine(replyMessage);
                return true;
            }

            return false;
        }

        public bool ForwardSearchResult(FileSearchResultModel searchResults)
        {
            throw new NotImplementedException();
        }
    }
}

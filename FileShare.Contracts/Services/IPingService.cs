using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FileShare.Domain.Models;

namespace FileShare.Contracts.Services
{
    [ServiceContract(CallbackContract = typeof(IPingService))]

    public interface IPingService
    {
        [OperationContract(IsOneWay = true)]

        void Ping(HostInfo info);

        [OperationContract(IsOneWay = true)]

        void SearchFiles(string searchTerm, string peerID);
    }
}

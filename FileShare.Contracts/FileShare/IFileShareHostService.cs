using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace FileShare.Contracts.FileShare
{
    [ServiceContract(CallbackContract = typeof(IFileShareServiceCallback), SessionMode = SessionMode.Required)]
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }

}

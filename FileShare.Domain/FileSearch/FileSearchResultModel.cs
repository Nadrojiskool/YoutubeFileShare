using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using FileShare.Domain.Models;

namespace FileShare.Domain.FileSearch
{
    [DataContract]
    public class FileSearchResultModel
    {
        [DataMember]
        public HostInfo ServiceHost { get; set; }
        [DataMember]
        public ObservableCollection<FileMetaData> Files { get; set; }
        [DataMember]
        public string PeerID { get; set; }
    }
}

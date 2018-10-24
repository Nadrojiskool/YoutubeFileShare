using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FileShare.Domain.Models
{
    [DataContract]
    public class FilePartModel
    {
        private readonly File _file;

        public FilePartModel(File file)
        {
            _file = file;
        }

        [DataMember]
        public FilePart FilePart { get; set; }
        [DataMember]
        public string FileID => _file.FileID;
        [DataMember]
        public byte[] FileBytes { get; set; }
    }
}

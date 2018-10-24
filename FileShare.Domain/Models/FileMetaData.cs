using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FileShare.Domain.Models
{
    [DataContract]
    public class FileMetaData
    {
        public FileMetaData(string fileID, string fileName, int fileLength)
        {
            FileID = fileID;
            FileName = fileName;
            FileLength = fileLength;
        }

        [DataMember]
        public string FileID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public int FileLength { get; }
    }
}

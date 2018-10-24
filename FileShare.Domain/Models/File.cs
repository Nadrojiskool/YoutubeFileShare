using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domain.Models
{
    public class File
    {
        public string FileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int FileLength { get; set; }
        public byte[] FileContent { get; set; }

        public FileMetaData GetFileMeta()
        {
            return new FileMetaData(FileID, FileName, FileLength);
        }
    }
}

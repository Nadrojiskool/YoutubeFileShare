using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.Models;

namespace FileShare.SampleData
{
    public static class FileSample
    {
        private static ObservableCollection<File> _availableFiles = new ObservableCollection<File>();
        private static ObservableCollection<FileMetaData> _metaDatas = new ObservableCollection<FileMetaData>();

        public static ObservableCollection<File> GetAvailableFiles()
        {
            if (! _availableFiles.Any())
            {
                _availableFiles.Add(new Task<File>(() => 
                {
                    var bytes = new byte[23234345];
                    var file = new File
                    {
                        FileID = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne",
                        FileContent = bytes,
                        FileLength = bytes.Length, 
                        FileType = "video/mp4"
                    };
                    _metaDatas.Add(file.GetFileMeta());
                    return file;
                }).Result);
            }

            return _availableFiles;
        }

        public static ObservableCollection<FileMetaData> GetFileMetaData()
        {
            if (! _metaDatas.Any())
            {
                GetAvailableFiles();
                return _metaDatas;
            }
            return _metaDatas;
        }
    }
}

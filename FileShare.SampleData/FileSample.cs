using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.Models;

namespace FileShare.SampleData
{
    public class FileSample
    {
        private ObservableCollection<File> _availableFiles = new ObservableCollection<File>();
        private ObservableCollection<FileMetaData> _metaDatas = new ObservableCollection<FileMetaData>();

        public ObservableCollection<File> GetAvailableFiles()
        {
            if (! _availableFiles.Any())
            {
                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[2347],
                    FileLength = 2347,
                    FileName = "Max Payne",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[2734],
                    FileLength = 2734,
                    FileName = "Social Bone",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[2134],
                    FileLength = 2134,
                    FileName = "Jungle Train",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[234],
                    FileLength = 234,
                    FileName = "Pico Mico",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[7634],
                    FileLength = 7634,
                    FileName = "Last Legend",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[7634],
                    FileLength = 7634,
                    FileName = "The Bear of Mingo",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[7634],
                    FileLength = 7634,
                    FileName = "Street Fighter",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[7634],
                    FileLength = 7634,
                    FileName = "Lost in Italy",
                    FileType = "video/mp4"
                });

                _availableFiles.Add(new File
                {
                    FileID = Guid.NewGuid().ToString().Split('-')[4],
                    FileContent = new byte[634],
                    FileLength = 634,
                    FileName = "The German Stunner",
                    FileType = "video/mp4"
                });

                /*_availableFiles.Add(new Task<File>(() => 
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
                }).Result);*/
            }

            return _availableFiles;
        }

        public ObservableCollection<FileMetaData> GetFileMetaData()
        {
            if (! _metaDatas.Any())
            {
                GetAvailableFiles().ToList().ForEach(p =>
                {
                    _metaDatas.Add(new FileMetaData(p.FileID, p.FileName, p.FileLength));
                });
                return _metaDatas;
            }
            return _metaDatas;
        }
    }
}

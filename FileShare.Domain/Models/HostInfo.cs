using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domain.Models
{
    public class HostInfo
    {
        public string ID { get; set; }
        public string Uri { get; set; }
        public int Port { get; set; }
    }
}

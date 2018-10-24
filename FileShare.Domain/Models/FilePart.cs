using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace FileShare.Domain.Models
{
    [DataContract]
    public class FilePart
    {
        public FilePart(int take, int skip = 0)
        {
            Take = take;
            Skip = skip;
        }

        [DataMember]
        public int Take { get; }
        [DataMember]
        public int Skip { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{
    
    public class FileDownloadCommand
    {
        public Guid FileId { get; set; }

        public string Description { get; set; }

        public DateTime UploadedDate { get; set; }

        public string SourceUserName { get; set; }

        public string SourceDeviceId { get; set; }

        public string FileName { get; set; }

        public long Size { get; set; }

        public byte[] Hash { get; set; }

        public int NumberParts { set; get; }
    }

    public class FileUploadCommand
    {
        public Guid FileId { get; set; }

        public string Description { get; set; }

        public string DestinationUserName { get; set; }

        public string FileName { get; set; }

        public long Size { get; set; }

        public byte[] Hash { get; set; }

        public int NumberParts { set; get; }
    }

}

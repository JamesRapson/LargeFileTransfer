using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{

    //TODO remove
    public class AvailableFileDetailsModel
    {
        public Guid Id { get; set; }

        public string UploadedFromDeviceId { get; set; }

        public string UploadedByUserName { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public DateTime UploadedDate { get; set; }

        public long Size { get; set; }

        public byte[] Hash { get; set; }
    }
}

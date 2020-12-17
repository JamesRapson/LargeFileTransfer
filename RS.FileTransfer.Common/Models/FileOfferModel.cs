using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{
    public class FileOfferModel
    {
        public string OfferedToUserName { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public long Size { get; set; }

        public int NumberOfFileParts { get; set; }

        public byte[] Hash { get; set; }
    }

}

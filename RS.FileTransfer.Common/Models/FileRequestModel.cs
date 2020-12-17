using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{
    public class FileRequestModel
    {
        public string RequestedFromUserName { get; set; }

        public string FileName { get; set; }
    }
}

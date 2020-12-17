using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Common.Models
{
    public class SendMessageModel
    {
        public string DestinationUserName { get; set; }

        public string Message { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Common.Models
{
    public class ReceivedMessageModel
    {
        public Guid Id { get; set; }

        public string SourceDeviceId { get; set; }

        public string SourceUserName { get; set; }

        public string Message { get; set; }

        public DateTime SentDate { get; set; }
    }
}
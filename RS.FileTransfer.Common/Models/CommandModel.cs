using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Common.Models
{
    public enum CommandTypeEnum
    {
        UploadFile,
        DownloadFile,
        ReceiveMessage,
        ConnectionRequest,
        ApprovedConnectionRequest
    }

    public class CommandModel
    {
        public string DestinationUserName { get; set; }

        public CommandTypeEnum CommandType { get; set; }

        public DateTime Date { get; set; }

        public Guid[] ItemIds { get; set; }
    }
}
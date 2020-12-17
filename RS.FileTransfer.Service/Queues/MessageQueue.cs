using RS.FileTransfer.Common;
using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Service.Queues
{

    public enum MessageType
    {
        UserMessage,
        ExecuteCommand,
        SendFile,
        ReceiveFile,
        ListFolder,
        TakePicture
    }

    public class MessageDetails : IQueueItem
    {
        public MessageDetails()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Key { get; set; }

        public string SourceDeviceId { get; set; }

        public string SourceUserName { get; set; }

        public string DestinationUserName { get; set; }

        public DateTime Date { get; set; }

        public string Value { get; set; }

        public MessageType MessageType { get; set; }
    }

    public class MessageQueue : BaseQueue<MessageDetails>
    {
        public List<Guid> GetUnReceivedMessages(string userName, string deviceId)
        {
            return GetUnreceivedIds(userName, deviceId, (x => x.DestinationUserName == userName));
        }
    }
}
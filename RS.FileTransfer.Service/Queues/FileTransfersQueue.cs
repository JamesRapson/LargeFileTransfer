using RS.FileTransfer.Common;
using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Service.Queues
{
    public class FilePart
    {
        public string FilePath { get; set; }

        public int PartNumber { get; set; }
    }

    public class FileDetails : IQueueItem
    {
        public FileDetails()
        {
            Id = Guid.NewGuid();
            FileParts = new List<FilePart>();
        }

        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string SourceDeviceId { get; set; }

        public string SourceUserName { get; set; }

        public string DestinationUserName { get; set; }

        public string DestinationDeviceId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public DateTime? UploadStartDate { get; set; }

        public DateTime? UploadCompletedDate { get; set; }

        public long Size { get; set; }

        public byte[] Hash { get; set; }

        public int NumberOfFileParts { get; set; }

        public List<FilePart> FileParts { get; set; }
    }

    public class FileTransfersQueue : BaseQueue<FileDetails>
    {
        List<ReceivedItemIds> ReceivedUploadCommandIds { get; set; }

        public FileTransfersQueue()
        {
            ReceivedUploadCommandIds = new List<ReceivedItemIds>();
        }

        public List<Guid> GetUnReceivedDownloadCommands(string userName, string deviceId)
        {
            string _userName = userName.ToLower();
            return GetUnreceivedIds(userName, deviceId, x => x.UploadCompletedDate.HasValue && (x.DestinationUserName.ToLower() == _userName));
        }

        
        public List<Guid> GetUnReceivedUploadCommands(string userName, string deviceId)
        {
            string _userName = userName.ToLower();

            var received = ReceivedUploadCommandIds.FirstOrDefault(x => (x.UserName == userName) && (x.DeviceId == deviceId));
            if (received == null)
            {
                received = new ReceivedItemIds() { UserName = userName, DeviceId = deviceId };
                ReceivedUploadCommandIds.Add(received);
            }
            var ids = received.ItemsIds;

            var results = Items.Where(x => !x.UploadCompletedDate.HasValue && (x.SourceUserName.ToLower() == _userName)).Where(x => !ids.Contains(x.Id)).Select(x => x.Id).ToList();
            received.ItemsIds.AddRange(results);
            return results;
        }
        
    }
}
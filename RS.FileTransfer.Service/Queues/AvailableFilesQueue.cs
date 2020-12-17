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

        public string UploadedFromDeviceId { get; set; }

        public string UploadedByUserName { get; set; }

        public string DestinationUserName { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        public bool UploadComplete { get; set; }

        public DateTime UploadCompletedDate { get; set; }

        public long Size { get; set; }

        public byte[] Hash { get; set; }

        public List<FilePart> FileParts { get; set; }
    }

    public class AvailableFilesQueue : BaseQueue<FileDetails>
    {
        public List<Guid> GetUnReceivedAvailableFiles(string userName, string deviceId)
        {
            return GetUnreceivedIds(userName, deviceId, (x => x.DestinationUserName == userName));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Service.Queues
{
    public static class QueueInstances
    {
        public static MessageQueue MessageQueue { get; set; }
        public static FileTransfersQueue FileTransfersQueue { get; set; }
        //public static AvailableFilesQueue AvailableFilesQueue { get; set; }

        static QueueInstances()
        {
            MessageQueue = new MessageQueue();
            FileTransfersQueue = new FileTransfersQueue();
            //AvailableFilesQueue = new AvailableFilesQueue();
        }

    }
}
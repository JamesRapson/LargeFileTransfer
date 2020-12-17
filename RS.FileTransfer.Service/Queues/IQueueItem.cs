using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service.Queues
{
    public interface IQueueItem
    {
        Guid Id { get; set; }

        DateTime Date { get; set; }
    }
}

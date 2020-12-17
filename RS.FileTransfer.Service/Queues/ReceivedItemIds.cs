using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service.Queues
{
    public class ReceivedItemIds
    {
        public ReceivedItemIds()
        {
            ItemsIds = new List<Guid>();
        }

        public string UserName { get; set; }

        public string DeviceId { get; set; }

        public List<Guid> ItemsIds { get; set; }
    }
}

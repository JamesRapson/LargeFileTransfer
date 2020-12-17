using RS.FileTransfer.Common;
using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS.FileTransfer.Service.Queues
{
    public class BaseQueue<T> where T : IQueueItem
    {
        protected List<T> Items { get; set; }

        protected List<ReceivedItemIds> ReceivedItemIds { get; set; }

        public BaseQueue()
        {
            Items = new List<T>();
            ReceivedItemIds = new List<ReceivedItemIds>();
        }

        public void Add(T item)
        {
            Items.Add(item);
        }

        public void Delete(Guid id)
        {
            Items.RemoveAll(x => x.Id == id);
        }

        public T Get(Guid id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> Get(IEnumerable<Guid> ids)
        {
            return Items.Where(x => ids.Contains(x.Id)).OrderBy(x => x.Date);
        }

        public List<Guid> GetUnreceivedIds(string userName, string deviceId, Func<T, bool> predicate)
        {
            var received = ReceivedItemIds.FirstOrDefault(x => (x.UserName == userName) && (x.DeviceId == deviceId));
            if (received == null)
            {
                received = new ReceivedItemIds() { UserName = userName, DeviceId = deviceId };
                ReceivedItemIds.Add(received);
            }
            var ids = received.ItemsIds;

            var results = Items.Where(predicate).Where(x => !ids.Contains(x.Id)).Select(x => x.Id).ToList();
            received.ItemsIds.AddRange(results);
            return results;
        }
    }
}
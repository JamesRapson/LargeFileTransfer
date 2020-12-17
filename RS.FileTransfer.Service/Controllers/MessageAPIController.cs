using RS.FileTransfer.Common.Models;
using RS.FileTransfer.Service.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RS.FileTransfer.Service.Controllers
{
    [Authorize]
    public class MessageAPIController : BaseAPIController
    {
        [HttpPost]
        [Route("api/message/send/{messageType}")]
        public Guid SendMessage([FromBody]SendMessageModel model, string messageType)
        {
            if ((model.DestinationUserName != UserName) &&(!Program.ConnectionsManager.ConnectionExists(UserName, model.DestinationUserName)))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to send message to the specified user" });
            
            MessageType type = MessageType.UserMessage;
            if (!string.IsNullOrEmpty(messageType) && !Enum.TryParse<MessageType>(messageType, out type))
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid Message Type" });
            
            var details = new MessageDetails()
            {
                Date = DateTime.Now,
                SourceDeviceId = DeviceId,
                SourceUserName = UserName,
                DestinationUserName = model.DestinationUserName,
                Value = model.Message,
                MessageType = type
            };

            QueueInstances.MessageQueue.Add(details);
#if DEBUG
            Console.WriteLine(string.Format("SendMessage : Created message from '{0}' to '{1}'", UserName, model.DestinationUserName));
#endif
            UserCommandsLock.Wake(model.DestinationUserName);
            return details.Id;
        }

        [HttpPost]
        [Route("api/message/wait")]
        public async Task<IEnumerable<ReceivedMessageModel>> WaitForMessages()
        {
            string deviceId = DeviceId;
            string userName = UserName;

            DateTime startDate = DateTime.Now;
            while (DateTime.Now.Subtract(startDate).TotalSeconds < 600)
            {
                var ids = QueueInstances.MessageQueue.GetUnReceivedMessages(userName, deviceId);
                if (ids.Count() > 0)
                    return Get(ids.ToArray());

                await UserCommandsLock.Wait(userName, DeviceId);
            }
            return null;
        }


        [HttpPost]
        [Route("api/message/get")]
        public IEnumerable<ReceivedMessageModel> Get([FromBody]Guid[] ids)
        {
            var list = new List<ReceivedMessageModel>();
            var items = QueueInstances.MessageQueue.Get(ids).Where(x => (x.DestinationUserName == UserName) && (x.MessageType == MessageType.UserMessage));
            foreach (var item in items)
            {
                list.Add(new ReceivedMessageModel()
                {
                    Id = item.Id,
                    SourceDeviceId = item.SourceDeviceId,
                    SourceUserName = item.SourceUserName,
                    Message = item.Value,
                    SentDate = item.Date
                });
            }
            return list;
        }
    }
}

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
    public class CommandAPIController : BaseAPIController
    {
        [HttpGet]
        [Route("api/command")]
        public async Task<CommandModel> WaitForCommand()
        {
            string deviceId = DeviceId;
            string userName = UserName;

            DateTime startDate = DateTime.Now;
            while (DateTime.Now.Subtract(startDate).TotalSeconds < 600)
            {
                var ids = QueueInstances.MessageQueue.GetUnReceivedMessages(userName, deviceId);
                if (ids.Count() > 0)
                {
#if DEBUG
                    Console.WriteLine("WaitForCommand : Returning ReceiveMessage command to User " + userName);
#endif
                    return new CommandModel()
                    {
                        CommandType = CommandTypeEnum.ReceiveMessage,
                        Date = DateTime.Now,
                        DestinationUserName = userName,
                        ItemIds = ids.ToArray()
                    };
                }

                ids = QueueInstances.FileTransfersQueue.GetUnReceivedDownloadCommands(userName, deviceId);
                if (ids.Count() > 0)
                {
#if DEBUG
                    Console.WriteLine("WaitForCommand : Returning download commandto User " + userName);
#endif
                    return new CommandModel()
                    {
                        CommandType = CommandTypeEnum.DownloadFile,
                        Date = DateTime.Now,
                        DestinationUserName = userName,
                        ItemIds = ids.ToArray()
                    };
                }

                ids = QueueInstances.FileTransfersQueue.GetUnReceivedUploadCommands(userName, deviceId);
                if (ids.Count() > 0)
                {
#if DEBUG
                    Console.WriteLine("WaitForCommand : Returning Upload command to User " + userName);
#endif
                    return new CommandModel()
                    {
                        CommandType = CommandTypeEnum.UploadFile,
                        Date = DateTime.Now,
                        DestinationUserName = userName,
                        ItemIds = ids.ToArray()
                    };
                }

                ids = Program.ConnectionRequestsManager.GetApprovedConnections(userName, deviceId);
                if (ids.Count() > 0)
                {
#if DEBUG
                    Console.WriteLine("WaitForCommand : Returning ApprovedConnectionRequest command to User " + userName);
#endif
                    return new CommandModel()
                    {
                        CommandType = CommandTypeEnum.ApprovedConnectionRequest,
                        Date = DateTime.Now,
                        DestinationUserName = userName,
                        ItemIds = ids.ToArray()
                    };
                }

                ids = Program.ConnectionRequestsManager.GetRequestedConnections(userName, deviceId);
                if (ids.Count() > 0)
                {
#if DEBUG
                    Console.WriteLine("WaitForCommand : Returning ConnectionRequest command to User " + userName);
#endif
                    return new CommandModel()
                    {
                        CommandType = CommandTypeEnum.ConnectionRequest,
                        Date = DateTime.Now,
                        DestinationUserName = userName,
                        ItemIds = ids.ToArray()
                    };
                }

                await UserCommandsLock.Wait(userName, DeviceId);

            }
            return null;
        }
    }
}

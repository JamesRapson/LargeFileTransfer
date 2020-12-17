using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using RS.FileTransfer.Service.Queues;

namespace RS.FileTransfer.Service.Controllers
{
    [Authorize]
    public class ConnectionsAPIController : BaseAPIController
    {
        //GetAllConnections
        // Request Connection to a specified user with an introduction message
        // Approve Copnnection Request
        // Get a ConnectionRequest

        [HttpGet]
        [Route("api/connection/all")]
        public List<UserConnectionModel> GetAllConnections()
        {
            var user = Program.UserManager.Get(UserName);
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Failed to find user record" });

            var list = new List<UserConnectionModel>();
            foreach (var userName in Program.ConnectionsManager.GetContactsForUser(UserName))
            {
                list.Add(new UserConnectionModel()
                {
                    UserName = userName,
                    Status = UserConnectionStatusEnum.Approved
                });
            }

            // get unapproved contact request made by this user to others
            foreach (var request in Program.ConnectionRequestsManager.GetRequestsFromUser(UserName))
            {
                if (list.Exists(x => x.UserName.ToLower() == request.ConnectToUserName.ToLower()))
                    continue;

                list.Add(new UserConnectionModel()
                {
                    UserName = request.ConnectToUserName,
                    Status = UserConnectionStatusEnum.RequestedByMe,
                    ConnectionRequestId = request.Id
                });
            }

            // get unapproved contact requests made by others to this user
            foreach (var request in Program.ConnectionRequestsManager.GetRequestsToUser(UserName))
            {
                if (list.Exists(x => x.UserName.ToLower() == request.RequestedByUserName.ToLower()))
                    continue;

                list.Add(new UserConnectionModel()
                {
                    UserName = request.RequestedByUserName,
                    Status = UserConnectionStatusEnum.RequestedByOther,
                    ConnectionRequestId = request.Id
                });
            }
            return list;
        }

        [HttpPost]
        [Route("api/connection/request")]
        public void RequestConnection([FromBody]CreateConnectionRequestModel model)
        {
            if (model.ConnectToUserName == UserName)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid UserName" });

            Program.ConnectionRequestsManager.RequestConnection(UserName, model.ConnectToUserName, model.IntroductionMessage);
#if DEBUG
            Console.WriteLine(string.Format("RequestConnection : ConnectionRequest created by User '{0}', connecting to user '{1}.", UserName, model.ConnectToUserName));
#endif
            UserCommandsLock.Wake(model.ConnectToUserName);
            UserCommandsLock.Wake(UserName);
        }

        [HttpPost]
        [Route("api/connection/request/get")]
        public IEnumerable<ConnectionRequestModel> GetConnectionRequests([FromBody]Guid[] ids)
        {
            var list = new List<ConnectionRequestModel>();
            foreach (var request in Program.ConnectionRequestsManager.Get(ids).Where(x => (x.ConnectToUserName == UserName) || (x.RequestedByUserName == UserName)))
            {
                list.Add(new ConnectionRequestModel()
                    {
                        Id = request.Id,
                        ConnectToUserName = request.ConnectToUserName,
                        RequestedByUserName = request.RequestedByUserName,
                        IntroductionMessage = request.IntroductionMessage,
                        Status = request.Status
                    });
            }
            return list;
        }

        [HttpPost]
        [Route("api/connection/request/setstatus")]
        public void SetConnectionRequestStatus([FromBody]ConnectionRequestUpdateStatusModel model)
        {
            var request = Program.ConnectionRequestsManager.Get(model.Id);
            if (request == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Invalid Id" });

            if (request.ConnectToUserName.ToLower() != UserName.ToLower())
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "You are not authorised to update the specified request" });

            Program.ConnectionRequestsManager.SetStatus(model.Id, model.Status);
#if DEBUG
            Console.WriteLine(string.Format("SetConnectionRequestStatus : ConnectionRequest status update to '{0}' by User '{1}', connecting to user '{2}.", model.Status, UserName, request.ConnectToUserName));
#endif

            if (model.Status == ConnectionRequestStatusEnum.Approved)
                Program.ConnectionsManager.Add(request.RequestedByUserName, UserName);

            UserCommandsLock.Wake(request.RequestedByUserName);
            UserCommandsLock.Wake(UserName);
        }
    }
}

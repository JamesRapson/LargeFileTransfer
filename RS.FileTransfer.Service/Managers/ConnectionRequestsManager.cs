using RS.FileTransfer.Common.Models;
using RS.FileTransfer.Service.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Service
{
    public class ConnectionRequest
    {
        ConnectionRequestStatusEnum _status;
        DateTime _createdDate;
        DateTime _updatedDate;

        public ConnectionRequest()
        {
            Id = Guid.NewGuid();
            _createdDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        public string RequestedByUserName { get; set; }

        public string ConnectToUserName { get; set; }

        public string IntroductionMessage { get; set; }

        public DateTime CreatedDate 
        {
            get { return _createdDate; }
        }

        public DateTime UpdatedDate
        {
            get { return _updatedDate; }
        }

        public ConnectionRequestStatusEnum Status
        {
            get { return _status; }
        }

        public void SetStatus(ConnectionRequestStatusEnum status)
        {
            _status = status;
            _updatedDate = DateTime.Now;
        }
    }

    public class ConnectionRequestsManager
    {
        List<ConnectionRequest> _connectionRequests = new List<ConnectionRequest>();

        List<ReceivedItemIds> ReceivedApprovalsIds { get; set; }

        List<ReceivedItemIds> ReceivedRequestsIds { get; set; }

        public ConnectionRequestsManager()
        {
            ReceivedApprovalsIds = new List<ReceivedItemIds>();
            ReceivedRequestsIds = new List<ReceivedItemIds>();
        }

        public ConnectionRequest Get(Guid id)
        {
            return _connectionRequests.FirstOrDefault(x => x.Id == id);
        }

        public List<ConnectionRequest> Get(IEnumerable<Guid> ids)
        {
            return _connectionRequests.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<ConnectionRequest> GetRequestsFromUser(string userName)
        {
            string _userName = userName.ToLower();
            return _connectionRequests.Where(x => (x.RequestedByUserName.ToLower() == _userName)).ToList();
        }

        public List<ConnectionRequest> GetRequestsToUser(string userName)
        {
            string _userName = userName.ToLower();
            return _connectionRequests.Where(x => (x.ConnectToUserName.ToLower() == _userName)).ToList();
        }

        public Guid RequestConnection(string requestedByUserName, string connectToUserName, string introductionMessage)
        {
            string _requestedByUserName = requestedByUserName.ToLower();
            string _connectToUserName = connectToUserName.ToLower();
            var request = _connectionRequests.FirstOrDefault(x => (x.RequestedByUserName.ToLower() == _requestedByUserName) && (x.ConnectToUserName.ToLower() == _connectToUserName));
            if (request != null)
            {
                // request already exists
                return request.Id;
            }

            request = _connectionRequests.FirstOrDefault(x => (x.RequestedByUserName.ToLower() == _connectToUserName) && (x.ConnectToUserName.ToLower() == _requestedByUserName));
            if (request != null)
            {   
                // the other user has already made a request - approve it
                request.SetStatus(ConnectionRequestStatusEnum.Approved);
                return request.Id;
            }

            request = new ConnectionRequest()
            {
                RequestedByUserName = requestedByUserName,
                ConnectToUserName = connectToUserName,
                IntroductionMessage = introductionMessage
            };

            _connectionRequests.Add(request);
            return request.Id;
        }

        public void SetStatus(Guid id, ConnectionRequestStatusEnum status)
        {
            var request = _connectionRequests.FirstOrDefault(x => (x.Id == id));
            if (request == null)
                throw new Exception("Invalid Id");

            request.SetStatus(status);
        }

        public List<Guid> GetApprovedConnections(string userName, string deviceId)
        {
            string _userName = userName.ToLower();
            var received = ReceivedApprovalsIds.FirstOrDefault(x => (x.UserName.ToLower() == _userName) && (x.DeviceId == deviceId));
            if (received == null)
            {
                received = new ReceivedItemIds() { UserName = userName, DeviceId = deviceId };
                ReceivedApprovalsIds.Add(received);
            }
            var ids = received.ItemsIds;

            var results = _connectionRequests.Where(x => 
                    (x.Status == ConnectionRequestStatusEnum.Approved) &&
                    ((x.RequestedByUserName.ToLower() == _userName) || (x.ConnectToUserName.ToLower() == _userName)) &&
                    !ids.Contains(x.Id)
                ).Select(x => x.Id).ToList();

            received.ItemsIds.AddRange(results);
            return results;
        }

        public List<Guid> GetRequestedConnections(string userName, string deviceId)
        {
            string _userName = userName.ToLower();
            var received = ReceivedRequestsIds.FirstOrDefault(x => (x.UserName.ToLower() == _userName) && (x.DeviceId == deviceId));
            if (received == null)
            {
                received = new ReceivedItemIds() 
                { 
                    UserName = userName, 
                    DeviceId = deviceId 
                };
                ReceivedRequestsIds.Add(received);
            }
            var ids = received.ItemsIds;

            var results = _connectionRequests.Where(x => 
                    (x.Status == ConnectionRequestStatusEnum.Requested) &&
                    ((x.RequestedByUserName.ToLower() == _userName) || (x.ConnectToUserName.ToLower() == _userName)) && 
                    !ids.Contains(x.Id)
                ).Select(x => x.Id).ToList();

            received.ItemsIds.AddRange(results);
            return results;
        }

        public void Load()
        {

        }

        public void Save()
        {

        }
    }
}

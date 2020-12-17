using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{
    public enum ConnectionRequestStatusEnum
    {
        Requested,
        Approved,
        Rejected
    }

    public class CreateConnectionRequestModel
    {
        public string ConnectToUserName { get; set; }

        public string IntroductionMessage { get; set; }
    }

    public class ConnectionRequestModel
    {
        public Guid Id { get; set; }

        public string RequestedByUserName { get; set; }

        public string ConnectToUserName { get; set; }

        public string IntroductionMessage { get; set; }

        public ConnectionRequestStatusEnum Status { get; set; }
    }

    public class ConnectionRequestUpdateStatusModel
    {
        public Guid Id { get; set; }

        public ConnectionRequestStatusEnum Status { get; set; }
    }
}

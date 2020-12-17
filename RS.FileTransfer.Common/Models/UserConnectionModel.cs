using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Common.Models
{
    public enum UserConnectionStatusEnum
    {
        RequestedByMe,
        RequestedByOther,
        Approved,
        Rejected,
        Blocked
    }

    public class UserConnectionModel
    {
        public string UserName { get; set; }

        public UserConnectionStatusEnum Status { get; set; }

        public Guid ConnectionRequestId { get; set; }
    }
}

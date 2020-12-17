using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.FileTransfer.Client
{
    public enum FileTransferCompletionStatus
    {
        NotStarted,
        Started,
        Successful,
        Error,
        Cancelled

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RS.FileTransfer.Service.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected string DeviceId
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.Claims.FirstOrDefault(x => x.Type == "deviceid");
                if (claim != null)
                    return claim.Value;
                return null;
            }
        }

        protected string UserName
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;
                var claim = identity.Claims.FirstOrDefault(x => x.Type == "user_name");
                if (claim != null)
                    return claim.Value;
                return null;
            }
        }
    }
}

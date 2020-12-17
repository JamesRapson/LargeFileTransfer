using RS.FileTransfer.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace RS.FileTransfer.Service.Controllers
{
    public class UserAPIController : BaseAPIController
    {
        [HttpPost]
        [Route("api/user/register")]
        public void RegisterUser([FromBody]RegisterUserModel model)
        {
            var user = Program.UserManager.Get(UserName);
            if (user != null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "The specified UserName already exists" });

            Program.UserManager.Add(new User()
            {
                UserName = model.UserName,
                Password = model.Password,
                RegistedDate = DateTime.Now
            });

#if DEBUG
            Console.WriteLine(string.Format("RegisterUser : New User Registration '{0}'.", model.UserName));
#endif
        }

        /* 
        [Authorize]
        [HttpPost]
        [Route("api/user/update")]
        public Guid UpdateUser([FromBody]UpdateUserDetails model)
        {

        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;

namespace RS.FileTransfer.Service.Providers
{

    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {       
            // This call is required. but we're not using client authentication, so validate and move on...
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = Program.UserManager.Get(context.UserName);
            if ((user == null) || !user.CheckPassword(context.Password))
            { 
                context.SetError("login failed", "The User Name or Password is incorrect.");
                //context.Rejected();
                return;
            }

            var data = await context.Request.ReadFormAsync();
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user_name", context.UserName));
            identity.AddClaim(new Claim("deviceid", data["deviceid"]));
            identity.AddClaim(new Claim("role", user.Role));

            // Identity info will ultimately be encoded into an Access Token as a result of this call:
            context.Validated(identity);
        }
    }
}

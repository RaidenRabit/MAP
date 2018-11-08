using System.Security.Claims;
using System.Threading.Tasks;
using MAPeApi.DataManagement;
using MAPeApi.Models;
using Microsoft.Owin.Security.OAuth;

namespace MAPeApi
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private UsersDM usersDM;

        public AuthorizationServerProvider()
        {
            usersDM = new UsersDM();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            User user = usersDM.Login(context.UserName, context.Password);
            if(user != null)
                if (user.UserId == 1)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
                    identity.AddClaim(new Claim("username", user.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Nickname));
                    context.Validated(identity);
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
                    identity.AddClaim(new Claim("username", user.Username));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Nickname));
                    context.Validated(identity);
                }
            else
            {
                context.SetError("invalid grant", "Wrong username or password");
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace API.AuthorizationFilters
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute()
        : base(typeof(AuthorizeActionFilter))
        {
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            if (actionContext.HttpContext.User.Identity.IsAuthenticated && actionContext.HttpContext.Request.Headers.ContainsKey("Claims"))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claim = actionContext.HttpContext.Request.Headers["Claims"].FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(claim))
                {
                    var jwtToken = tokenHandler.ReadJwtToken(claim);

                    var userName = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

                    if (!string.IsNullOrEmpty(userName))
                    {
                        var genericIdentity = new GenericIdentity(userName);
                        genericIdentity.AddClaims(jwtToken.Claims);

                        if (actionContext.HttpContext.Request.Headers.ContainsKey("LanguageId"))
                        {
                            var languageId = actionContext.HttpContext.Request.Headers["LanguageId"].First();
                            genericIdentity.AddClaim(new Claim("NewLanguageId", languageId));

                        }
                        var genericPrincipal = new GenericPrincipal(genericIdentity, null);
                        Thread.CurrentPrincipal = genericPrincipal;
                        actionContext.HttpContext.User.AddIdentity(genericIdentity);
                        return;
                    }
                }
            }
            else
            {
                actionContext.Result = new ForbidResult();
            }
        }
    }
}
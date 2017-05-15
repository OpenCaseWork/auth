using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenCaseWork.AuthServer
{
    public class CustomProfileService: IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>();
            //claims.Add(new Claim(JwtClaimTypes.WebSite, "https://leastprivilege.com"));
            claims.Add(new Claim("FullName", "Joe Tester"));
            //claims.Add(new Claim("CustomClaim2", "SomeValue"));
            context.IssuedClaims = claims;
            return Task.FromResult(0);
        }
        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(0);
        }
    }
}

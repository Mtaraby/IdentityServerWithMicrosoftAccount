using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fiver.Security.AuthServer
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        //private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        //private readonly UserManager<ApplicationUser> _userManager;

      

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            var claims = context.Subject.Claims;            

            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
          //  var sub = context.Subject.GetSubjectId();
           // var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = true;
        }
    }
}

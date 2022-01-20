using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarMarket.Server.Infrastructure
{
    public class IdentityServerProfileService : IProfileService
    {
        protected UserManager<UserModel> _userManager;
        protected IUserService _userService;

        public IdentityServerProfileService(UserManager<UserModel> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userService.GetByEmailAsync(GetUserEmailByClaims(context.Subject.Claims));

            var claims = await GetClaimsAsync(user);

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userService.GetByEmailAsync(GetUserEmailByClaims(context.Subject.Claims));

            context.IsActive = user != null;
        }

        private async Task<ICollection<Claim>> GetClaimsAsync(UserModel user)
        {
            var claims = new List<Claim>();
            IList<string> roles;

            try
            {
                roles = await _userManager.GetRolesAsync(user);
            }
            catch
            {
                return claims;
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private string GetUserEmailByClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (claim.Type.Equals("sub"))
                {
                    return claim.Value;
                }
            }

            return string.Empty;
        }
    }
}

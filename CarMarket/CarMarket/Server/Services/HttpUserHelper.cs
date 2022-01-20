using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Services
{
    public class HttpUserHelper
    {
        public static Task<UserModel> GetCurrentUserAsync(IUserService userService, HttpContext context) =>
            userService.GetByEmailAsync(GetCurrentUserEmail(context));

        private static string GetCurrentUserEmail(HttpContext context)
        {
            context.Request.Headers.TryGetValue("Authorization", out StringValues values);

            var token = values.FirstOrDefault().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var userEmail = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            return userEmail;
        }
    }
}

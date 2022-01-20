using Microsoft.AspNetCore.Identity;

namespace CarMarket.Core.User.Domain
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IdentityRole<string> Role { get; set; }
    }
}
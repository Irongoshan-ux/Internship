using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        Task<string> CreateAsync(UserModel userModel);
        Task<UserModel> GetAsync(string userId);
        Task<UserModel> GetByEmailAsync(string email);
        Task<DataResult<UserModel>> GetByPageAsync(int skip = 0, int take = 5);
        Task<List<UserModel>> GetAllAsync();
        Task DeleteAsync(string userId);
        Task<UserModel> AuthenticateAsync(string email, string password);
        Task<IdentityRole> GetRoleAsync(string roleName);
        Task UpdateUserAsync(string userId, UserModel userModel);
        Task AddUserToRoleAsync(UserModel user, string roleName);
    }
}

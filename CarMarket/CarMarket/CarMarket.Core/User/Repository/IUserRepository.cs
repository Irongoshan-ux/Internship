using System.Collections.Generic;
using System.Threading.Tasks;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using Microsoft.AspNetCore.Identity;

namespace CarMarket.Core.User.Repository
{
    public interface IUserRepository
    {
        Task<UserModel> FindByIdAsync(string id);
        Task<string> SaveAsync(UserModel user);
        Task<List<UserModel>> FindAllAsync();
        Task DeleteAsync(string userId);
        Task<UserModel> FindUserModelAsync(string email, string password);
        Task<UserModel> FindByEmailAsync(string email);
        Task<IdentityRole> FindRoleAsync(string roleName);
        Task UpdateAsync(string userId, UserModel userModel);
        Task<DataResult<UserModel>> FindByPageAsync(int skip, int take);
        Task AddUserToRoleAsync(UserModel user, IdentityRole role);
    }
}
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.User
{
    public interface IHttpUserService : IHttpService<UserModel, string>
    {
        Task AddPermissionAsync(string userId, params Permission[] permissions);
        Task ChangePermissionAsync(string userId, Permission replaceablePermission, Permission substitutePermission);
        Task<UserModel> GetByEmailAsync(string email);
        Task DeletePermissionAsync(string userId, Permission permission);
    }
}

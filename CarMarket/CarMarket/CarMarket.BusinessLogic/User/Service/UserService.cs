using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.BusinessLogic.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserModel> _userManager;

        public UserService(IUserRepository userRepository, UserManager<UserModel> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            return await _userRepository.FindUserModelAsync(email, password);
        }

        public async Task<string> CreateAsync(UserModel userModel)
        {
            if (userModel is null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

            userModel.Id = Guid.NewGuid().ToString();

            return await _userRepository.SaveAsync(userModel);
        }

        public async Task<UserModel> GetAsync(string userId)
        {
            return await _userRepository.FindByIdAsync(userId);
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }

        public async Task DeleteAsync(string userId)
        {
            var userModel = await _userRepository.FindByIdAsync(userId);

            if (userModel is null)
            {
                throw new ArgumentException(nameof(userModel) + " shouldn't be null");
            }

            await _userRepository.DeleteAsync(userModel.Id);
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            UserModel user;
            try
            {
                user = await _userRepository.FindByEmailAsync(email);
            }
            catch
            {
                return null;
            }

            return user;
        }

        public async Task<IdentityRole> GetRoleAsync(string roleName)
        {
            return await _userRepository.FindRoleAsync(roleName);
        }

        public async Task UpdateUserAsync(string userId, UserModel userModel)
        {
            await _userRepository.UpdateAsync(userId, userModel);
        }

        public async Task<DataResult<UserModel>> GetByPageAsync(int skip = 0, int take = 5)
        {
            var users = await _userRepository.FindByPageAsync(skip, take);

            foreach (var user in users.Data)
            {
                user.Role = new()
                {
                    Name = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
                };
            }

            return users;
        }

        public async Task AddUserToRoleAsync(UserModel user, string roleName)
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                var tempUser = await _userRepository.FindByEmailAsync(user.Email);
                user.Id = tempUser.Id;
            }

            var role = await _userRepository.FindRoleAsync(roleName);

            await _userRepository.AddUserToRoleAsync(user, role);
        }
    }
}

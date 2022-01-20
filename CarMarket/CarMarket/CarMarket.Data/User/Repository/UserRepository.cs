using AutoMapper;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Data.User.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Data.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
        }

        public async Task<UserModel> FindUserModelAsync(string email, string password)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => (x.Email == email) && (x.PasswordHash == password));

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<UserModel> FindByIdAsync(string id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<UserModel>>(userEntities);
        }

        public async Task<string> SaveAsync(UserModel userModel)
        {
            var newUserEntity = _mapper.Map<UserEntity>(userModel);
            
            var added = await _context.Users.AddAsync(newUserEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(string userId)
        {
            var userEntity = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<IdentityRole> FindRoleAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
        }

        public async Task AddUserToRoleAsync(UserModel user, IdentityRole role)
        {
            _context.UserRoles.Add(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(string userId, UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);

            _context.Update(userEntity);

            await _context.SaveChangesAsync();            
        }

        public async Task<DataResult<UserModel>> FindByPageAsync(int skip, int take)
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .OrderBy(x => x.Email)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            var result = new DataResult<UserModel>
            {
                Data = _mapper.Map<IEnumerable<UserModel>>(userEntities),
                Count = await _context.Users.CountAsync()
            };

            return result;
        }
    }
}

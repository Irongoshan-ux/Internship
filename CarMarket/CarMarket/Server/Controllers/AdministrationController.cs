using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using CarMarket.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IUserService _userService;

        public AdministrationController(
            IUserService userService,
            RoleManager<IdentityRole> roleManager,
            UserManager<UserModel> userManager,
            ILogger<CarController> logger)
        {
            _userService = userService;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = roleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest($"Role {roleName} already exists.");
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            return Ok(_roleManager.Roles.ToList());
        }

        [HttpGet("GetUsersInRole")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            if (!await UserIsInAdminRole())
            {
                return BadRequest("Access denied");
            }

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                return NotFound($"Role with name={roleName} not found");
            }

            var usersInRole = new List<UserModel>();

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    usersInRole.Add(user);
                }
            }

            return Ok(usersInRole);
        }

        private async Task<bool> UserIsInAdminRole()
        {
            try
            {
                var currentUser = await GetCurrentUserAsync();

                return await _userManager.IsInRoleAsync(currentUser, "Admin");
            }
            catch
            {
                return false;
            }
        }

        private async Task<UserModel> GetCurrentUserAsync() => await HttpUserHelper.GetCurrentUserAsync(_userService, HttpContext);
    }
}

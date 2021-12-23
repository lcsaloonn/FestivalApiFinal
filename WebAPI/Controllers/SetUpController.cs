using System.Linq;
using System.Threading.Tasks;
using Domain;
using Infrastructure.SqlServer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    //api/setup
    [Route("api/[controller]")] 
    [ApiController]
    public class SetUpController:ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetUpController> _logger; //use to display message in console or database

        public SetUpController(Context context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<SetUpController> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        
        /*
         * the table Role has been created with the asp.net Identity with the initial migrations 
         */

        [HttpGet]
        [Route("GetAllRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        [Route("PostRoles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string name)
        {
            //Check if the role exist
            var roleExists = await _roleManager.RoleExistsAsync(name);
            if (!roleExists)
            {
                //add the role
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));
                //check if the role has been add
                if (roleResult.Succeeded)
                {
                    //String interpolation $
                    _logger.LogInformation($"The Role{name} has been added");
                    return Ok(new
                    {
                        result = $"The role {name} has been added"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role{name} has not been added");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }
            }

            return BadRequest(new {error = "Role already exist"});

        }

        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUserToRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> AddUserToRole(string name, string roleName)
        {
            //check user exist
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                _logger.LogInformation($"The user {name} does not exist");
                //retuning an anonymus object
                return BadRequest(new
                {
                    error="user does not exist"
                });
            }
            //check the role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new
                {
                    error="role does not exist"
                });
            }
            //ad role to user
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success"
                });

            }
            else
            {
                _logger.LogInformation($"The user was not able to be added");
                return BadRequest(new
                {
                    error="The user was not able to be added"
                }); 
            }
            

        }
         [HttpGet]
        [Route("GetUserRoles")] 
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string userName)
        {
            //check if the email is valid
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                _logger.LogInformation($"The user {userName} does not exist");
                //retuning an anonymus object
                return BadRequest(new
                {
                    error="user does not exist"
                });
            }
            
            //return roles
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpDelete]
        [Route("RemoveUserFromRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveUserFromRole(string name , string roleName)
        {
            //check user exist
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                _logger.LogInformation($"The user {name} does not exist");
                //retuning an anonymus object
                return BadRequest(new
                {
                    error="user does not exist"
                });
            }
            //check the role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                _logger.LogInformation($"The role {roleName} does not exist");
                return BadRequest(new
                {
                    error="role does not exist"
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user,roleName);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User {name} has been remove from {roleName}"
                });
            }
            else
            {
                return BadRequest(new
                {
                    error =$"Unable to remove User {name} from{roleName}"
                });
            }
        }
    }
}
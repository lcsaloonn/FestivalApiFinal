using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.authentication;

using Infrastructure.configuration;
using Infrastructure.SqlServer.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.configuration;

namespace WebAPI.Controllers
{
    //apiManagement 
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthManagementController:ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthManagementController> _logger;
        private readonly Context _context;
        
        


        public AuthManagementController(UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> optionsMonitor
            ,RoleManager<IdentityRole> roleManager,ILogger<AuthManagementController> logger, Context context)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)// si bien required et 250 caractère
            {
                //pseudo unnique ToDo verifier FindByNameAsync
                var existingUser = await _userManager.FindByNameAsync(user.Pseudo);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "Pseudo déja utilisé"
                        },
                        IsSuccess = false
                    });
                }
                
                //Creation
                var newUser = new IdentityUser()
                {
                    UserName = user.Pseudo
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                
                // add the user to a role
                 await _userManager.AddToRoleAsync(newUser, "AppUser");
                
                if (isCreated.Succeeded)
                {
                    //var jwtToken = GenerateJwtToken(newUser);

                    return Ok();
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x =>x.Description).ToList(),
                        IsSuccess = false
                    });
                    
                }
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalide"
                },
                IsSuccess = false
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.Pseudo);
                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "invalide login"
                        }, IsSuccess = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                            "invalide login"
                        }, IsSuccess = false
                    });
                }
                //test-----------------------------------
                 var jwtTokenHandler = new JwtSecurityTokenHandler();
                  var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
                  
                  var claims = new List<Claim>
                  {
                      new Claim("Id", existingUser.Id),
                      new Claim(JwtRegisteredClaimNames.Sub, existingUser.UserName),
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                  };
                  var userClaims = await _userManager.GetClaimsAsync(existingUser);
                  claims.AddRange(userClaims);
            
                  // Get the user role and add it to the claims
                  
                  var userRoles = await _userManager.GetRolesAsync(existingUser);
            
                  foreach(var userRole in userRoles)
                  {
                      var role = await _roleManager.FindByNameAsync(userRole);
            
                      if(role != null)
                      {
                          claims.Add(new Claim(ClaimTypes.Role, userRole));
            
                          var roleClaims = await _roleManager.GetClaimsAsync(role);
                          foreach(var roleClaim in roleClaims)
                          {
                              claims.Add(roleClaim);
                          }
                      }
                  }
                  var tokenDescriptor = new SecurityTokenDescriptor
                  {
                      Subject = new ClaimsIdentity(claims),
                      Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
                      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
              
                  };

                  var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                  var jwtToken = jwtTokenHandler.WriteToken(token);
                  
                  await _context.SaveChangesAsync();
     

                  
                  
                //-----------------------------------------------
               
                //var jwtToken = GenerateJwtToken(existingUser);

                return Ok( new Authentication() {
                    Token = jwtToken,
                    IsSuccess = true,
                      
                });


            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalide",
                },
                IsSuccess = false
            });
        }
        
        //--------------------------------------- CAUSE PROBLEME -----------------------------------------
        // Get all valid claims for the corresponding user
        private async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            //Getting the claims that we have assigned to the user
                       var userClaims = await _userManager.GetClaimsAsync(user);
                       claims.AddRange(userClaims);
            
                        // Get the user role and add it to the claims
                        var userf = await _userManager.FindByNameAsync("Test");
                        var userRoles = await _userManager.GetRolesAsync(userf);
            
                        foreach(var userRole in userRoles)
                        {
                            var role = await _roleManager.FindByNameAsync(userRole);
            
                            if(role != null)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, userRole));
            
                                var roleClaims = await _roleManager.GetClaimsAsync(role);
                                foreach(var roleClaim in roleClaims)
                                {
                                    claims.Add(roleClaim);
                                }
                            }
                        }
        
            return claims;
            
        }

        //function creation toker
        private async Task<Authentication> GenerateJwtToken(IdentityUser user)
        {
           var jwtTokenHandler = new JwtSecurityTokenHandler();

           var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

           //test----------------------------
           
           var claims = new List<Claim>
           {
               new Claim("Id", user.Id),
               new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              
           };
           
           //---------------------
          
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
              
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

          

         //   await _apiDbContext.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
     

            return new Authentication() {
                Token = null,
                IsSuccess = true,
            };
        }
        
    }
}
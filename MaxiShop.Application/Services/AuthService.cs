using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using MaxiShop.Application.ViewModels;

namespace MaxiShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private ApplicationUser ApplicationUser;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            ApplicationUser = new();
        } 

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            ApplicationUser.FirstName = register.FirstName;
            ApplicationUser.LastName = register.LastName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(ApplicationUser,register.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "CUSTOMER");
            }

            return result.Errors;
        }

        public async Task<object> Login(Login login)
        {
            ApplicationUser = await _userManager.FindByEmailAsync(login.Email);

            if(ApplicationUser == null)
            {
                return "Invalid Email Address";
            }

            var result = await _signInManager.PasswordSignInAsync(ApplicationUser, login.Password, isPersistent: true, lockoutOnFailure: true);

            var isValidCredential = await _userManager.CheckPasswordAsync(ApplicationUser, login.Password);

            if (result.Succeeded)
            {
                var token = await GenerateToken();

                LoginResponse loginResponse = new LoginResponse
                {
                    UserId = ApplicationUser.Id,
                    Token = token
                };

                return loginResponse;
            }
            else
            {
                if (result.IsLockedOut)
                {
                    return "Your Account is Locked,Contact System Admin";
                }

                if (result.IsNotAllowed)
                {
                    return "Please Verify Email Address";
                }

                if(isValidCredential == false)
                {
                    return "Invalid Password";
                }
                else
                {
                    return "Login Failed";
                }
            }
        }
    
    
        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(ApplicationUser);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,ApplicationUser.Email)
            }.Union(roleClaims).ToList();

            var token = new JwtSecurityToken
                (
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims:claims,
                signingCredentials:signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

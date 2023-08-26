using MaxiShop.Application.Common;
using MaxiShop.Application.InputModels;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser ApplicationUser;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            ApplicationUser = new();
        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
        {
            ApplicationUser.FirstName = register.FirstName;
            ApplicationUser.LastName = register.LastName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;

            var result = await _userManager.CreateAsync(ApplicationUser);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "ADMIN");
            }

            return result.Errors;
        }
    }
}

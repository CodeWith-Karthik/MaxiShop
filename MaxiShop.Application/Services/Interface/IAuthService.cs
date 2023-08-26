using MaxiShop.Application.InputModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interface
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Register(Register register);
    }
}

using MaxiShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Domain.Contracts
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task UpdateAsync(Brand brand);
    }
}

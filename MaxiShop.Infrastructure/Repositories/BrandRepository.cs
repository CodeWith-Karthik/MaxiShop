using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand> , IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task UpdateAsync(Brand brand)
        {
            _dbContext.Update(brand);
            await _dbContext.SaveChangesAsync();
        }
    }
}

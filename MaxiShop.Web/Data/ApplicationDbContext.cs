using MaxiShop.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MaxiShop.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Category { get; set; }
    }

}

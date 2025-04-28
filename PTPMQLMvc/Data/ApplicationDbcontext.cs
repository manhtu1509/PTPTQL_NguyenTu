using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Models;

namespace PTPMQLMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Các DbSet cho các bảng khác trong cơ sở dữ liệu
        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employees { get; set; }
        
    }
}

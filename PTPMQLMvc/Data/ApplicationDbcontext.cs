using Microsoft.EntityFrameworkCore;
using PTPMQLMvc.Models; 

namespace PTPMQLMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; } 
    }
}

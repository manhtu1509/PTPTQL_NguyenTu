using Microsoft.EntityFrameworkCore;

namespace PTPMQLMvc;

public partial class YourDatabaseContext : DbContext
{
    public YourDatabaseContext()
    {
    }

    public YourDatabaseContext(DbContextOptions<YourDatabaseContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlite("Data Source=your_database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

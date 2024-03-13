using Microsoft.EntityFrameworkCore;

namespace tech_test_payment.infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    protected ApplicationDbContext()
    { }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "tech-test-payment.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

}

using Microsoft.EntityFrameworkCore;
using MyFarmAPI.Configurations;
using MyFarmAPI.Models.Entity;

namespace MyFarmAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Cow> Cows => Set<Cow>();
    public DbSet<MilkEntry> MilkEntries => Set<MilkEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CowConfiguration());
        modelBuilder.ApplyConfiguration(new MilkEntryConfiguration());
    }
}
using Microsoft.EntityFrameworkCore;

public class ApiDb : DbContext
{
    public ApiDb(DbContextOptions<ApiDb> options)
        : base(options) { }

    public DbSet<Perfume> Perfumes => Set<Perfume>();
    public DbSet<Bottle> Bottles => Set<Bottle>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Split> Splits => Set<Split>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
    }
}
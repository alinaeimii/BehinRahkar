using Microsoft.EntityFrameworkCore;
using Product.Core.Entites;

namespace Product.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
    {

    }
    public DbSet<Products> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Products>()
           .Property(b => b.CreationDate)
         .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Products>()
        .Property(b => b.Name)
        .IsRequired()
        .HasMaxLength(200);

        modelBuilder.Entity<Products>()
        .Property(b => b.Price)
         .IsRequired();

    }
}


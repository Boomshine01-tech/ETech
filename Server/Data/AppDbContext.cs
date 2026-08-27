using Microsoft.EntityFrameworkCore;
using ETechEnergie.Shared.Models;

namespace ETechEnergie.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Service> Services { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ContactRequest> ContactRequests { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Formation> Formations { get; set; }
    public DbSet<Inscription> Inscriptions { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<RealisationSection> RealisationSections { get; set; }
    public DbSet<RealisationImage> RealisationImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.DiscountPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RealisationImage>()
            .HasOne(i => i.Section)
            .WithMany(s => s.Images)
            .HasForeignKey(i => i.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RealisationSection>()
            .HasIndex(s => s.Slug)
            .IsUnique();

        modelBuilder.Entity<Service>()
            .HasIndex(s => s.Name)
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}

using AutomatedTestingEfCore.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace AutomatedTestingEfCore.Persistence;

public class BloggerContext : DbContext
{
    public BloggerContext(DbContextOptions<BloggerContext> options)
        : base(options)
    {
    }

    public DbSet<BlogPost> Blogs { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BlogPost>().HasOne<Person>();
    }
}
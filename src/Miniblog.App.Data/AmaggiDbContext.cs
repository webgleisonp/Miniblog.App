using Miniblog.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Miniblog.App.Data;

public class MiniblogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public MiniblogDbContext(DbContextOptions<MiniblogDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

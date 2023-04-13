using MemoryCacheWebAPI_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryCacheWebAPI_Demo.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<Order> Orders { get; set; }
}
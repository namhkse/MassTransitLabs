using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Models;

namespace Ordering.Api.Database;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

     public DbSet<Order> Orders => Set<Order>();
     
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         base.OnModelCreating(modelBuilder);

         modelBuilder.AddInboxStateEntity();
         modelBuilder.AddOutboxMessageEntity();
         modelBuilder.AddOutboxStateEntity();
     }
}
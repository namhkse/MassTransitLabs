using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Database.Configurations;
using Ordering.Api.Models;

namespace Ordering.Api.Database;


public class AppDbContext : SagaDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

     public DbSet<Order> Orders => Set<Order>();
     
     protected override IEnumerable<ISagaClassMap> Configurations
     {
         get
         {
             yield return new OrderSagaMap();
         }
     }
     
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         base.OnModelCreating(modelBuilder);

         modelBuilder.AddInboxStateEntity();
         modelBuilder.AddOutboxMessageEntity();
         modelBuilder.AddOutboxStateEntity();
     }
}
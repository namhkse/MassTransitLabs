using MassTransit;
using Ordering.Api.Database;
using Ordering.Api.Models;

namespace Ordering.Api.Consumers;

public record CreateOrder(string CustomerName, decimal TotalAmount);
    
public class CreateOrderConsumer(AppDbContext db, ILogger<CreateOrderConsumer> logger, IPublishEndpoint endpoint) : IConsumer<CreateOrder>
{
    public async Task Consume(ConsumeContext<CreateOrder> context)
    {
        var command = context.Message;
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerName = command.CustomerName,
            TotalAmount = command.TotalAmount,
            CreatedAt = DateTime.UtcNow
        };
            
        db.Orders.Add(order);

        await endpoint.Publish(new OrderCreated(order.Id));

        await db.SaveChangesAsync();
        
        logger.LogInformation("Order is created {Id}", order.Id);
        
    }
}

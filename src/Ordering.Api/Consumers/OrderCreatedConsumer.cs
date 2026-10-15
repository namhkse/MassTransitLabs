using MassTransit;

namespace Ordering.Api.Consumers;

public record OrderCreated(Guid Id);

public class OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var id = context.Message.Id;
        logger.LogInformation("🔔 Order created {Id}", id);
    }
}
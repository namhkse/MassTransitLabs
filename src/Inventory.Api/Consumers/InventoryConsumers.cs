using Shop.Contracts;

namespace Inventory.Api.Consumers;

using MassTransit;

public class ReserveInventoryConsumer(
    ILogger<ReserveInventoryConsumer> logger)
    : IConsumer<ReserveInventory>
{
    public Task Consume(ConsumeContext<ReserveInventory> context)
    {
logger.LogInformation(
    "Reserving inventory for order {OrderId}",
    context.Message.CorrelationId);

logger.LogInformation(
    "Inventory reservation process: {Progress}%",
    90);

logger.LogInformation(
    "Inventory reserved for order {OrderId}",
    context.Message.CorrelationId);
        return context.Publish(new InventoryReserved
        {
            CorrelationId = context.Message.CorrelationId
        });
    }
}

public class ReleaseInventoryConsumer(
    ILogger<ReleaseInventoryConsumer> logger)
    : IConsumer<ReleaseInventory>
{
    public async Task Consume(ConsumeContext<ReleaseInventory> context)
    {
logger.LogInformation(
    "Releasing inventory for order {OrderId}",
    context.Message.CorrelationId);

logger.LogInformation(
    "Inventory release processing: {Status}",
    "Done");

logger.LogInformation(
    "Inventory released for order {OrderId}",
    context.Message.CorrelationId);

        await context.Publish(new InventoryReleased
        {
            CorrelationId = context.Message.CorrelationId
        });
    }
}
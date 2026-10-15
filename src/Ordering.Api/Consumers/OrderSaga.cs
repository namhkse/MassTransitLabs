using MassTransit;
using Shop.Contracts;

namespace Ordering.Api.Consumers;

public class OrderSaga : 
    ISaga, 
    InitiatedBy<SubmitOrder>,
    Orchestrates<InventoryReserved>,
    Orchestrates<PaymentFailed>,
    Orchestrates<InventoryReleased>
{
    public Guid CorrelationId { get; set; }
    public string Status { get; set; } = "Pending";
    public string? FailureReason { get; set; }
    
    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {
        Status = "ReservingInventory";
        await context.Publish(new ReserveInventory()
        {
            CorrelationId = CorrelationId
        });
    }

    public async Task Consume(ConsumeContext<InventoryReserved> context)
    {
        Status = "ProcessingPayment";
        await context.Publish(new ChargePayment()
        {
            CorrelationId = CorrelationId
        });
    }

    public async Task Consume(ConsumeContext<PaymentFailed> context)
    {
        Status = "ReleasingInventory";
        FailureReason = context.Message.Reason;
        await context.Publish(new ReleaseInventory()
        {
            CorrelationId = CorrelationId
        });
    }

    public async Task Consume(ConsumeContext<InventoryReleased> context)
    {
        Status = "Failed";
    }
}
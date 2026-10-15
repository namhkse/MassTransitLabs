using MassTransit;

namespace Shop.Contracts;

// api -> saga
public record SubmitOrder : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

// saga -> inventory service
public record ReserveInventory : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

// inventory -> saga
public record InventoryReserved : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

// saga -> payment
public record ChargePayment : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

// payment -> saga
public record PaymentFailed : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
    public string? Reason { get; init; }  
}

// saga -> inventory service (compensation)
public record ReleaseInventory : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

// inventory -> saga
public record InventoryReleased : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; init; }  
}

using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Api.Consumers;

namespace Ordering.Api.Database.Configurations;

public class OrderSagaMap : SagaClassMap<OrderSaga>
{
    protected override void Configure(
        EntityTypeBuilder<OrderSaga> entity,
        ModelBuilder model)
    {
        entity.Property(x => x.Status);
        entity.Property(x => x.FailureReason);
    }
}
using Shop.Contracts;

namespace Payment.Api.Consumers;

using MassTransit;

public class ChargePaymentConsumer(
    ILogger<ChargePaymentConsumer> logger)
    : IConsumer<ChargePayment>
{
    public Task Consume(ConsumeContext<ChargePayment> context)
    {
logger.LogInformation(
    "Processing payment for order {OrderId}",
    context.Message.CorrelationId);

logger.LogInformation(
    "Payment processing progress: {Progress}%",
    0);

logger.LogError(
    "Payment failed for order {OrderId}",
    context.Message.CorrelationId);

        return context.Publish(new PaymentFailed
        {
            CorrelationId = context.Message.CorrelationId,
            Reason = "Insufficient funds"
        });
    }
}
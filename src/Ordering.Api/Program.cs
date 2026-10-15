using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Consumers;
using Ordering.Api.Database;
using Shop.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var connectionString =
    builder.Configuration.GetConnectionString("ordering");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ordering", false));
    
    // Register consumers.
    x.AddConsumers(typeof(Program).Assembly);
    
    // Register sagas.
    x.AddSaga<OrderSaga>()
        .EntityFrameworkRepository(r =>
    {
        r.ExistingDbContext<AppDbContext>();
        r.UsePostgres();
    });
    // x.AddEntityFrameworkOutbox<AppDbContext>(o =>
    // {
    //     o.QueryDelay = TimeSpan.FromSeconds(10);
    //     o.UsePostgres();
    //     o.UseBusOutbox();
    // });

    // x.AddConfigureEndpointsCallback((context, name, cfg) => { cfg.UseEntityFrameworkOutbox<AppDbContext>(context); });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("rabbitmq")!));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/", () => "API is running");

app.MapPost("/orders", async (IPublishEndpoint publishEndpoint) =>
{
    var orderId = Guid.NewGuid();

    await publishEndpoint.Publish(new SubmitOrder
    {
        CorrelationId = orderId,
    });

    return Results.Accepted(
        $"/orders/{orderId}",
        new { OrderId = orderId });
});
//
// app.MapPost("/orders/{id:guid}/accept", async (Guid id, IPublishEndpoint publishEndpoint) =>
//     {
//         await publishEndpoint.Publish(new AcceptOrder
//         {
//             CorrelationId = id,
//             Timestamp = DateTime.UtcNow
//         });
//
//         return Results.Accepted(value: new
//         {
//             OrderId = id,
//             Status = "Acceptance message published"
//         });
//     });

app.Run();
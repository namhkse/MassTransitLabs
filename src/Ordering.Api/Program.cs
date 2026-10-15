using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Consumers;
using Ordering.Api.Database;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ordering", false));

    x.AddConsumers(typeof(Program).Assembly);
    
    x.AddEntityFrameworkOutbox<AppDbContext>(o =>
    {
        o.QueryDelay = TimeSpan.FromSeconds(10);
        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.AddConfigureEndpointsCallback((context, name, cfg) => { cfg.UseEntityFrameworkOutbox<AppDbContext>(context); });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMQ")!));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapGet("/", () => "API is running");

app.MapPost("/api/orders", async (IPublishEndpoint endpoint, CreateOrder command, AppDbContext db) =>
{
    await endpoint.Publish(command);
    await db.SaveChangesAsync();
    return Results.Accepted();
});

app.Run();
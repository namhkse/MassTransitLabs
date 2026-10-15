using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMassTransit(x =>
{
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("payment", false));
    
    // Register consumers.
    x.AddConsumers(typeof(Program).Assembly);
    
    // Register sagas.
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("rabbitmq")!));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();

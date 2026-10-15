var builder = DistributedApplication.CreateBuilder(args);

var ordering = builder.AddConnectionString("ordering");
var rabbitmq = builder.AddConnectionString("rabbitmq");

builder.AddProject<Projects.Inventory_Api>("inventory-api")
    .WithReference(rabbitmq);

builder.AddProject<Projects.Ordering_Api>("ordering-api")
    .WithReference(ordering)
    .WithReference(rabbitmq);

builder.AddProject<Projects.Payment_Api>("payment-api")
    .WithReference(rabbitmq);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var tododatabase = builder.AddPostgresContainer("postgres").AddDatabase("tododatabase");

var cache = builder.AddRedis("cache");

var storage = builder.AddAzureStorage("storage").UseEmulator();

var queues = storage.AddQueues("queues");

var apiService = builder.AddProject<Projects.AspireTodo_ApiService>("apiservice")
    .WithReference(queues)
    .WithReference(tododatabase);

builder.AddProject<Projects.AspireTodo_Web>("webfrontend")
    .WithReference(queues)
    .WithReference(apiService);

builder.AddProject<Projects.AspireTodo_TodoDatabaseManager>("tododatabasemanager")
    .WithReference(tododatabase);

builder.Build().Run();

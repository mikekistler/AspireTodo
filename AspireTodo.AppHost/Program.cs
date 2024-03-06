var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var storage = builder.AddAzureStorage("storage").UseEmulator();

var queues = storage.AddQueues("queues");

var apiService = builder.AddProject<Projects.AspireTodo_ApiService>("apiservice")
    .WithReference(queues);

builder.AddProject<Projects.AspireTodo_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(queues)
    .WithReference(apiService);

builder.Build().Run();

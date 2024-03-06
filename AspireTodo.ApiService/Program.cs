var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add Azure Storage Queues
builder.AddAzureQueueService("queues");

// Add the QueueWorker
builder.Services.AddHostedService<QueueWorker>();

// Wire in the database
builder.AddNpgsqlDbContext<TodoDatabaseDbContext>("tododatabase");

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Http Api that returns the full list of todos.
app.MapGet("/todos", (TodoDatabaseDbContext ctx) => ctx.TodoItems.ToArray());

app.MapDefaultEndpoints();

app.Run();

// how the API models a TodoItem object
record TodoItem(string Description, bool IsCompleted) { }

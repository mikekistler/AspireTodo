var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// A static list of TodoItems to get us started
List<TodoItem> todoItems = new List<TodoItem>
{
    new("Build the API", false),
    new("Build the Frontend", false),
    new("Deploy the app", false)
};

// Http Api that returns the full list of todos.
app.MapGet("/todos", () => todoItems);

app.MapDefaultEndpoints();

app.Run();

// how the API models a TodoItem object
record TodoItem(string Description, bool IsCompleted) { }

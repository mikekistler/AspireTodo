using AspireTodo.TodoDatabaseManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add the database context
builder.AddNpgsqlDbContext<TodoDatabaseDbContext>("tododatabase", null,
    optionsBuilder => optionsBuilder.UseNpgsql(npgsqlBuilder =>
        npgsqlBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

// Add OTel, and wire up the database initialization's "migration" activity
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(DatabaseInitializer.ActivitySourceName));

// Add the database initialization service as a background worker
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<DatabaseInitializer>());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();

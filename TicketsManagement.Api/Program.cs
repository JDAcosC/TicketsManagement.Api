using TicketsManagement.Application;
using TicketsManagement.Infrastructure;
using TicketsManagement.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var maxRetries = 5;
var delay = TimeSpan.FromSeconds(10);
for (int i = 0; i < maxRetries; i++)
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<TicketsManagement.Infrastructure.Persistence.AppDbContext>();

            await db.Database.EnsureCreatedAsync();

            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"Applying {pendingMigrations.Count()} pending migrations...");
                await db.Database.MigrateAsync();
                Console.WriteLine("Migrations applied successfully.");
            }
            else
            {
                Console.WriteLine("Database is up to date.");
            }
        }
        Console.WriteLine("Database initialization successful.");
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database not ready. Attempt {i + 1} of {maxRetries}. Retrying in {delay.TotalSeconds} seconds...");
        if (i == maxRetries - 1)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
            throw;
        }
        await Task.Delay(delay);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
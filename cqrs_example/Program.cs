using cqrs_example;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CQRSDBContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("database");
    
    options.UseSqlite(connectionString);
});

builder.Services.AddTransient<CQRSDBContext>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();
builder.Services.AddScoped<ICommandValidator, CommandValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // migrate database, only during development
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<CQRSDBContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

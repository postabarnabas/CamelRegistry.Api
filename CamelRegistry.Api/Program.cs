using CamelRegistry.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CamelDbContext>(options =>
    options.UseSqlite("Data Source=camels.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDbContext>();
    db.Database.EnsureCreated();
}

app.Run();

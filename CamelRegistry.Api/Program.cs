using CamelRegistry.Api.Data;
using Microsoft.EntityFrameworkCore;
using CamelRegistry.Api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<CamelDbContext>(options =>
        options.UseSqlite("Data Source=camels.db"));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDbContext>();
    db.Database.EnsureCreated();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapPost("/camels", async (Camel camel, CamelDbContext db) =>
{
    if (camel.HumpCount is < 1 or > 2)
    {
        return Results.BadRequest("HumpCount csak 1 vagy 2 lehet.");
    }

    db.Camels.Add(camel);
    await db.SaveChangesAsync();

    return Results.Created($"/camels/{camel.Id}", camel);
});
app.MapGet("/camels", async (CamelDbContext db) =>
{
    return await db.Camels.ToListAsync();
});
app.MapGet("/camels/{id:int}", async (int id, CamelDbContext db) =>
{
    var camel = await db.Camels.FindAsync(id);

    return camel is not null
        ? Results.Ok(camel)
        : Results.NotFound();
});
app.MapPut("/camels/{id:int}", async (int id, Camel updatedCamel, CamelDbContext db) =>
{
    var camel = await db.Camels.FindAsync(id);

    if (camel is null)
        return Results.NotFound();

    if (updatedCamel.HumpCount is < 1 or > 2)
        return Results.BadRequest("HumpCount csak 1 vagy 2 lehet.");

    camel.Name = updatedCamel.Name;
    camel.Color = updatedCamel.Color;
    camel.HumpCount = updatedCamel.HumpCount;
    camel.LastFed = updatedCamel.LastFed;

    await db.SaveChangesAsync();
    return Results.Ok(camel);
});
app.MapDelete("/camels/{id:int}", async (int id, CamelDbContext db) =>
{
    var camel = await db.Camels.FindAsync(id);

    if (camel is null)
        return Results.NotFound();

    db.Camels.Remove(camel);
    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.Run();
public partial class Program { }

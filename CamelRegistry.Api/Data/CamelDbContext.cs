using Microsoft.EntityFrameworkCore;
using CamelRegistry.Api.Models;

namespace CamelRegistry.Api.Data;

public class CamelDbContext : DbContext
{
    public CamelDbContext(DbContextOptions<CamelDbContext> options)
        : base(options)
    {
    }

    public DbSet<Camel> Camels => Set<Camel>();
}

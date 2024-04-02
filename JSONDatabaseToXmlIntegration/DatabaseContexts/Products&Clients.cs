using Microsoft.EntityFrameworkCore;

namespace JSONDatabaseToXmlIntegration.DatabaseContexts;
using Models;

public class Products_Clients : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }

    public Products_Clients(DbContextOptions<Products_Clients> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductsAndClients;Username=postgres;Password=");
    }
    
}
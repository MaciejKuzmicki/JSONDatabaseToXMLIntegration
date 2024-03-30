namespace JSONDatabaseToXmlIntegration.DatabaseContexts;
using Models;

public class Products_Clients : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Client> Clients { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ProductsAndClients;Username=;Password=;");
    }
    
}
using JSONDatabaseToXmlIntegration.Models;
using Microsoft.EntityFrameworkCore;

namespace JSONDatabaseToXmlIntegration.DatabaseContexts;

public class FinalOrders : DbContext
{
    public DbSet<FinalOrderDetails> Orders { get; set; }
    public DbSet<ProductQuantity> ProductQuantities { get; set; }

    public FinalOrders(DbContextOptions<FinalOrders> options) : base(options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        contextOptionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FinalOrders;Username=postgres;Password=");
    }
    
}
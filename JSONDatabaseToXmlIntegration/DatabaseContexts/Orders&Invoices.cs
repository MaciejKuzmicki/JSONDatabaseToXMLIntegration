namespace JSONDatabaseToXmlIntegration.DatabaseContexts;
using Models;

public class Orders_Invoices : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OrdersAndInvoices;Username=;Password=;");
    }
}
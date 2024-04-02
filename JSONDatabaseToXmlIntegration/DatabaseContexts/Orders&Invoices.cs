using Microsoft.EntityFrameworkCore;

namespace JSONDatabaseToXmlIntegration.DatabaseContexts;
using Models;

public class Orders_Invoices : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    
    public Orders_Invoices(DbContextOptions<Orders_Invoices> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OrdersAndInvoices;Username=postgres;Password=");
    }
}
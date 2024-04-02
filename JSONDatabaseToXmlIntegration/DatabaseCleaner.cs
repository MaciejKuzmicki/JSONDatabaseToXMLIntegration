using JSONDatabaseToXmlIntegration.DatabaseContexts;

namespace JSONDatabaseToXmlIntegration;

public class DatabaseCleaner
{
    private readonly Orders_Invoices _ordersInvoices;
    private readonly Products_Clients _productsClients;
    private readonly FinalOrders _finalOrders;

    
    public DatabaseCleaner(Orders_Invoices ordersInvoices, Products_Clients productsClients, FinalOrders finalOrders)
    {
        _ordersInvoices = ordersInvoices;
        _productsClients = productsClients;
        _finalOrders = finalOrders;
    }

    public async Task ClearDatabase()
    {
        try
        {
            _finalOrders.Orders.RemoveRange(_finalOrders.Orders);
            await _finalOrders.SaveChangesAsync();
            _ordersInvoices.Orders.RemoveRange(_ordersInvoices.Orders);
            _ordersInvoices.Invoices.RemoveRange(_ordersInvoices.Invoices);
            await _ordersInvoices.SaveChangesAsync();
            _productsClients.Clients.RemoveRange(_productsClients.Clients);
            _productsClients.Products.RemoveRange(_productsClients.Products);
            await _productsClients.SaveChangesAsync();
            Console.WriteLine("Successfully cleaned all data");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
    }
}
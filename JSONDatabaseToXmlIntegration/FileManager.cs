using System.Text.Json;
using JSONDatabaseToXmlIntegration.DatabaseContexts;
using JSONDatabaseToXmlIntegration.Models;

namespace JSONDatabaseToXmlIntegration;

public class FileManager
{
    private readonly Orders_Invoices _ordersInvoices;
    private readonly Products_Clients _productsClients;
    
    public FileManager(Orders_Invoices ordersInvoices, Products_Clients productsClients)
    {
        _ordersInvoices = ordersInvoices;
        _productsClients = productsClients;
    }

    public async Task ReadClients(string path)
    {
        try
        {
            string json = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
            List<Client> clients = JsonSerializer.Deserialize<List<Client>>(json, options) ?? new List<Client>();
            if (clients.Count > 0)
            {
                foreach (var client in clients)
                {
                    Client existingClient = _productsClients.Clients.FirstOrDefault(x => x.ClientId == client.ClientId);
                    if (existingClient == null)
                    {
                        _productsClients.Clients.Add(client);
                    }
                    else
                    {
                        _productsClients.Clients.Remove(existingClient);
                        Console.WriteLine("Client with given id appeared more than once, deleted clients with id: " + existingClient.ClientId);
                    }
                }
            }

            await _productsClients.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
    }

    public async Task ReadProducts(string path)
    {
        try
        {
            string json = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Product existingProduct =
                        _productsClients.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                    if (existingProduct == null)
                    {
                        _productsClients.Products.Add(product);
                    }
                    else
                    {
                        _productsClients.Products.Remove(existingProduct);
                        Console.WriteLine("Product with given id appeared more than once, deleted products with id: " + existingProduct.ProductId);
                    }
                }
            }

            await _productsClients.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
    }
    
    public async Task ReadOrders(string path)
    {
        try
        {
            string json = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(json, options) ?? new List<Order>();
            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    Order existingOrder = _ordersInvoices.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
                    if (existingOrder == null)
                    {
                        _ordersInvoices.Orders.Add(order);
                    }
                    else
                    {
                        _ordersInvoices.Orders.Remove(existingOrder);
                        Console.WriteLine("Order with given id appeared more than once, deleted orders with id: " + existingOrder.OrderId);
                    }
                }
            }

            await _ordersInvoices.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
    }
    
    public async Task ReadInvoices(string path)
    {
        try
        {
            string json = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            };
            List<Invoice> invoices = JsonSerializer.Deserialize<List<Invoice>>(json, options) ?? new List<Invoice>();
            if (invoices.Count > 0)
            {
                foreach (var invoice in invoices)
                {
                    Invoice existingInvoice =
                        _ordersInvoices.Invoices.FirstOrDefault(x => x.InvoiceId == invoice.InvoiceId);
                    if(existingInvoice == null) {
                        _ordersInvoices.Invoices.Add(invoice);
                    }
                    else
                    {
                        _ordersInvoices.Invoices.Remove(existingInvoice);
                        Console.WriteLine("Invoice with given id appeared more than once, deleted invoices with id: " + existingInvoice.InvoiceId);
                    }
                }
            }

            await _ordersInvoices.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex.Message}");
        }
    }
}

   

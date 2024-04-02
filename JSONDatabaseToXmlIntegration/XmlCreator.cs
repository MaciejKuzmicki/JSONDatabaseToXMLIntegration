using System.Xml.Linq;
using JSONDatabaseToXmlIntegration.DatabaseContexts;
using JSONDatabaseToXmlIntegration.Models;
using Microsoft.EntityFrameworkCore;

namespace JSONDatabaseToXmlIntegration;

public class XmlCreator
{
    private readonly Orders_Invoices _ordersInvoices;
    private readonly Products_Clients _productsClients;
    private readonly FinalOrders _finalOrders;
    
    public XmlCreator(Orders_Invoices ordersInvoices, Products_Clients productsClients, FinalOrders finalOrders)
    {
        _ordersInvoices = ordersInvoices;
        _productsClients = productsClients;
        _finalOrders = finalOrders;
    }

    public async Task GenerateXML(string xmlPath)
    {
        List<FinalOrderDetails> finalOrderDetailsList = await _finalOrders.Orders.Include(a=>a.Products).ThenInclude(aa=>aa.Product).Include(b=>b.Client).Include(c=>c.Invoice).Include(d=>d.Products).ToListAsync();
        if (finalOrderDetailsList.Count == 0)
        {
            Console.WriteLine("There is no data to process");
            return;
        }
        
        XDocument doc = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Orders",
                finalOrderDetailsList.Select(order => 
                    new XElement("Order",
                        new XAttribute("Id", order.FinalOrderDetailsId),
                        new XAttribute("Date", order.OrderDate.ToString("yyyy-MM-dd")),
                        new XElement("Client",
                            new XAttribute("Name", order.Client.Name),
                            new XAttribute("Email", order.Client.Email)
                        ),
                        new XElement("Products",
                            order.Products.Select(p =>
                                new XElement("Product",
                                    new XAttribute("Id", p.Product.ProductId),
                                    new XAttribute("Name", p.Product.Name),
                                    new XAttribute("Price", p.Product.Price.ToString("0.00")),
                                    new XAttribute("Quantity", p.Quantity)
                                )
                            )
                        ),
                        new XElement("Total", order.Invoice.AmountDue.ToString("0.00"))
                    )
                )
            )
        );
        doc.Save(xmlPath);
    }

    public async Task PrepareData()
    {
        List<Order> orders = await _ordersInvoices.Orders.ToListAsync();
        if (orders.Count == 0)
        {
            Console.WriteLine("There is no data to process");
            return;
        }

        foreach (var order in orders)
        {
            try
            {
                var client = _productsClients.Clients.FirstOrDefault(x => x.ClientId == order.ClientId);
                var invoice = _ordersInvoices.Invoices.FirstOrDefault(x => x.OrderId == order.OrderId);
                if (client == null)
                {
                    Console.WriteLine("Order with id " + order.OrderId + " is connected with the not existing user");
                }

                if (invoice == null)
                {
                    Console.WriteLine("Order with id " + order.OrderId + " is connected with the not existing invoice");
                }

                List<ProductQuantity> productQuantities = new List<ProductQuantity>();
                foreach (var guid in order.Products)
                {
                    var existingProductQuantity = productQuantities.FirstOrDefault(pq => pq.Product.ProductId == guid);

                    if (existingProductQuantity != null)
                    {
                        existingProductQuantity.Quantity += 1;
                    }
                    else
                    {
                        productQuantities.Add(new ProductQuantity
                        {
                            Product = _productsClients.Products.FirstOrDefault(x=>x.ProductId == guid),
                            ProductQuantityId = Guid.NewGuid(),
                            Quantity = 1
                        });
                    }

                }

                FinalOrderDetails orderPrepared = new FinalOrderDetails()
                {
                    Client = client,
                    Invoice = invoice,
                    Products = productQuantities,
                    OrderDate = order.OrderDate
                };
                _finalOrders.Orders.Add(orderPrepared);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            await _finalOrders.SaveChangesAsync();
        }
    }
    
    
}
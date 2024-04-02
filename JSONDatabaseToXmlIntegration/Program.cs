using System;
using JSONDatabaseToXmlIntegration;
using JSONDatabaseToXmlIntegration.DatabaseContexts;
using JSONDatabaseToXmlIntegration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        if (args.Length > 0 && args[0] == "-DeleteAllData")
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderScoped = scope.ServiceProvider;
                var databaseCleaner = serviceProviderScoped.GetService<DatabaseCleaner>();
                await databaseCleaner.ClearDatabase();
            }
        }
        else if (args.Length > 1 && args[0] == "-GenerateXML")
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderScoped = scope.ServiceProvider;
                var xmlCreator = serviceProviderScoped.GetService<XmlCreator>();
                await xmlCreator.PrepareData();
                await xmlCreator.GenerateXML(args[1]);
            }
        }
        else if(args.Length > 0 && args[0] == "-AddData")
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var serviceProviderScoped = scope.ServiceProvider;
                var fileManager = serviceProviderScoped.GetService<FileManager>();
                if (args[1] == "-Products&Clients" && args.Length > 3 && fileManager != null)
                {
                    await fileManager.ReadProducts(args[2]);
                    await fileManager.ReadClients(args[3]);
                }
                else if (args[1] == "-Orders&Invoices" && args.Length > 3 && fileManager != null)
                {
                    await fileManager.ReadOrders(args[2]);
                    await fileManager.ReadInvoices(args[3]);
                }
            }
        }
        else
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  [command] [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  -DeleteAllData                  Clears all data from the application. Use with caution.");
            Console.WriteLine("  -AddData                        Adds data to the application from specified files.");
            Console.WriteLine("  -GenerateXML                    Generates an XML file with the current data.");
            Console.WriteLine();
            Console.WriteLine("Options for -AddData:");
            Console.WriteLine("  -Products&Clients [productFile] [clientFile]    Adds products and clients to the application from the specified files.");
            Console.WriteLine("  -Orders&Invoices [orderFile] [invoiceFile]      Adds orders and invoices to the application from the specified files.");
            Console.WriteLine();
        }
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<Products_Clients>(options =>
            options.UseNpgsql("Your Connection String Here"));
        services.AddDbContext<Orders_Invoices>(options =>
            options.UseNpgsql("Your Connection String Here"));
        services.AddDbContext<FinalOrders>(options =>
            options.UseNpgsql("Your Connection String Here"));

        services.AddTransient<FileManager>();
        services.AddTransient<DatabaseCleaner>();
        services.AddTransient<XmlCreator>();

    }

   
}

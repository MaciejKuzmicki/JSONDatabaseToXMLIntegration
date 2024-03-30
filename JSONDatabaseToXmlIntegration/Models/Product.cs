using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class Product
{
    [Key]
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}
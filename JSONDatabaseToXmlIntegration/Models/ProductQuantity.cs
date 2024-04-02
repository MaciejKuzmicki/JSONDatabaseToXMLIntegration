using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class ProductQuantity
{
    [Key]
    public Guid ProductQuantityId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class ProductQuantity
{
    [Key]
    public Guid ProductQuantityId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
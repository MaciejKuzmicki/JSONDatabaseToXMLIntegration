using System.ComponentModel.DataAnnotations;

namespace JSONDatabaseToXmlIntegration.Models;

public class Order
{
    [Key]
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime OrderDate { get; set; }
    public IList<Guid> Products { get; set; }
}